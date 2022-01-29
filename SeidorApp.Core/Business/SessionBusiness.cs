using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SeidorApp.Core.Models;
using SeidorApp.Core.Repository;
using SeidorApp.Core.Utility;
using SeidorCore.DataBase.Factory;
using SeidorCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeidorApp.Core.Business
{
    public class SessionBusiness
    {
        private readonly ILogger _logger;
        private readonly SessionRepository _sessionRepository;
        private readonly UserRepository _userRepository;
        private readonly string _sessionKey;

        public SessionBusiness(IDBConnectionFactory connectionFactory, ILogger<SessionBusiness> logger, ILoggerFactory loggerFactory, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _sessionRepository = new SessionRepository(connectionFactory, new Logger<SessionRepository>(loggerFactory));
            _userRepository = new UserRepository(connectionFactory, new Logger<UserRepository>(loggerFactory));
            _sessionKey = httpContextAccessor.HttpContext.Request.Headers["Session"];
        }

        public Response<string> DoLogin(User user)
        {
            Response<string> response = new Response<string>();

            Request request = new Request();
            request.filters.AddRange(new List<Filter>()
            {
                new Filter()
                {
                    Target1 = nameof(User.Email),
                    Value1 = user.Email,
                    OperationType = FilterOperationType.Equals,
                    AggregateType = FilterAggregateType.AND
                },
                new Filter()
                { 
                    Target1 = nameof(User.Password),
                    Value1 = LoginUtility.EncryptPassword(user.Password),
                    OperationType = FilterOperationType.Equals,
                    AggregateType = FilterAggregateType.AND                

                }
            });

            ListResponse<User> userResponse = _userRepository.FindByRequest(request);
            if (userResponse.HasAnyMessages)
            {
                response.Merge(userResponse);
                return response;
            }

            if (!userResponse.Data.Any())
            {
                response.AddValidationMessage("002", "Usuário não encontrado1");
                return response;
            }

            string sessionKey = LoginUtility.EncryptSession(user.Name, user.Email);
            Session session = new Session()
            {
                UserId = user.Id,
                Key =  sessionKey,
                LastUse = DateTime.UtcNow
            };

            Response<long> sessionResponse = _sessionRepository.Insert(session);
            if (sessionResponse.HasAnyMessages)
            {
                response.Merge(userResponse);
                return response;
            }
            CloseRepositories();

            response.Data = sessionKey;
            return response;
        }

        public Response<bool> DoLogout()
        {
            Response<bool> response = new Response<bool>();

            response = _sessionRepository.Delete(_sessionKey);
            CloseRepositories();

            return response;
        }

        public Response<bool> ValidateSession()
        {
            Response<bool> response = new Response<bool>();

            Request request = new Request();
            request.filters.Add(new Filter()
            {
                Target1 = "Key",
                Value1 = _sessionKey,
                OperationType = FilterOperationType.Equals
            });

            ListResponse<Session> sessionResponse =  _sessionRepository.FindByRequest(request);
            if (sessionResponse.HasAnyMessages)
            {
                response.Merge(sessionResponse);
                return response;
            }

            if (!sessionResponse.Data.Any())
            {
                response.Data = false;
                response.AddValidationMessage("002", "A sessão é inválida. Faça login para continuar.");
                return response;
            }

            CloseRepositories();
            response.Data = true;
            return response;
        }

        private void CloseRepositories()
        {
            _sessionRepository.CloseConnection();
            _userRepository.CloseConnection();
        }
    }
}
