using Microsoft.Extensions.Logging;
using SeidorApp.Core.Models;
using SeidorApp.Core.Repository;
using SeidorApp.Core.Utility;
using SeidorApp.Core.Validators;
using SeidorCore.DataBase.Factory;
using SeidorCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeidorApp.Core.Business
{
    public class UserBusiness
    {
        private readonly UserRepository _repository;
        private readonly UserValidator _validator;
        private readonly ILogger _logger;

        public UserBusiness(IDBConnectionFactory dbConnectionFactory, ILogger<UserBusiness> logger, ILoggerFactory loggerFactory)
        {
            _logger = logger;
            _repository = new UserRepository(dbConnectionFactory, new Logger<UserRepository>(loggerFactory));
            _validator = new UserValidator(_repository, new Logger<UserValidator>(loggerFactory));
        }

        public Response<long> Insert(User user)
        {
            Response<long> response = new Response<long>();
            _validator.ValidateInsert(response, user);

            if (response.HasValidationMessages)
                return response;

            user.Password = LoginUtility.EncryptPassword(user.Password);

            response = _repository.Insert(user);
            _repository.CloseConnection();

            return response;
        }

        public Response<bool> Update(User user)
        {
            Response<bool> response = new Response<bool>();
            _validator.ValidateUpdate(response, user);


            if (response.HasValidationMessages)
                return response;

            user.Password = LoginUtility.EncryptPassword(user.Password);

            response = _repository.Update(user);
            _repository.CloseConnection();

            return response;
        }

        public ListResponse<User> FindUserByRequest(Request request)
        {
            return _repository.FindByRequest(request);
            _repository.CloseConnection();

        }

        public ListResponse<User> FindUserById(long userId)
        {
            ListResponse<User> response = new ListResponse<User>();
            if(userId == 0)
            {
                response.AddValidationMessage("003", "UserId não pode ser menor ou igual a 0.");
                return response;
            }

            Request request = new Request();
            request.filters.Add(new Filter
            {
                Target1 = "Id",
                Value1 = userId,
                OperationType = FilterOperationType.Equals
            });

            response = _repository.FindByRequest(request);
            _repository.CloseConnection();

            return response;
        }


    }
}
