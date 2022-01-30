using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BaseCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BaseCore.Interfaces;
using BaseCore.Extensions;

namespace BaseCore.ApiControllers
{
    public class NDBaseController : ControllerBase
    {
        private readonly ILogger _logger;
        protected readonly IAuth _authService;
        public NDBaseController(ILogger<NDBaseController> logger, IAuth AuthService)
        {
            _authService = AuthService;
            _logger = logger;
        }
        protected ActionResult<Response<T>> GetResponse<T>(Func<Response<T>> method, bool requireAuthentication = true)
        {
            Response<T> response = new Response<T>();
            if (requireAuthentication)
            {
                Response<IUser> authResponse = _authService.ValidateSession();
                if (authResponse.HasAnyMessages)
                {
                    response.Merge(authResponse);
                    return response;
                }
                if (authResponse.Data.IsNull() || authResponse.Data.Id == 0) return response;
            }

            Task<Response<T>> task = new Task<Response<T>>(method);
            task.RunSynchronously();

            response = task.Result;

            return response.StatusCode switch
            {
                HttpStatusCode.OK => Ok(response),
                HttpStatusCode.BadRequest => BadRequest(response),
                _ => Ok(response)
            };
        }

        protected ActionResult<ListResponse<T>> GetListResponse<T>(Func<ListResponse<T>> method, bool requireAuthentication = true)
        {
            ListResponse<T> response = new ListResponse<T>();

            if (requireAuthentication)
            {
                Response<IUser> authResponse = _authService.ValidateSession();
                if (authResponse.HasAnyMessages)
                {
                    response.Merge(authResponse);
                    return response;
                }
                if (authResponse.Data.IsNull() || authResponse.Data.Id == 0) return response;
            }

            Task<ListResponse<T>> task = new Task<ListResponse<T>>(method);
            task.RunSynchronously();

            response = task.Result;

            return response.StatusCode switch
            {
                HttpStatusCode.OK => Ok(response),
                HttpStatusCode.BadRequest => BadRequest(response),
                _ => Ok(response)
            };
        }

        protected ActionResult<Response<T>> GetErrorResponse<T>(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest(new Response<T>
            {
                Messages = new()
                {
                    {
                        new()
                        {
                            Code = "900",
                            Text = "Algo deu errado. Contate o suporte",
                            MessageType = MessageType.FatalErrorException
                        }
                    }
                }
            });
        }

        protected ActionResult<ListResponse<T>> GetErrorListResponse<T>(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest(new ListResponse<T>
            {
                Messages = new()
                {
                    {
                        new()
                        {
                            Code = "900",
                            Text = "Algo deu errado. Contato o suporte",
                            MessageType = MessageType.FatalErrorException
                        }
                    }
                }
            });
        }
    }
}
