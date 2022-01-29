using Microsoft.AspNetCore.Mvc;
using SeidorCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SeidorCore.ApiControllers
{
    public class NDBaseController:ControllerBase
    {
        protected ActionResult<Response<T>> GetResponse<T>(Func<Response<T>> method)
        {
            Task<Response<T>> task = new Task<Response<T>>(method);
            task.RunSynchronously();

            Response<T> response = task.Result;

            return response.StatusCode switch
            {
                HttpStatusCode.OK => Ok(response),
                HttpStatusCode.BadRequest => BadRequest(response),
                _ => Ok(response)
            };
        }

        protected ActionResult<ListResponse<T>> GetListResponse<T>(Func<ListResponse<T>> method)
        {
            Task<ListResponse<T>> task = new Task<ListResponse<T>>(method);
            task.RunSynchronously();

            ListResponse<T> response = task.Result;

            return response.StatusCode switch
            {
                HttpStatusCode.OK => Ok(response),
                HttpStatusCode.BadRequest => BadRequest(response),
                _ => Ok(response)
            };
        }

        protected ActionResult<Response<T>> GetErrorResponse<T>()
        {

            return BadRequest(new Response<T>
            {
                Messages = new()
                {
                    {
                        new()
                        {
                            Code = "900",
                            Text = "Something went wrong",
                            MessageType = MessageType.FatalErrorException
                        }
                    }
                }
            });
        }

        protected ActionResult<ListResponse<T>> GetErrorInquiryResponse<T>()
        {
            return BadRequest(new ListResponse<T>
            {
                Messages = new()
                {
                    {
                        new()
                        {
                            Code = "900",
                            Text = "Something went wrong",
                            MessageType = MessageType.FatalErrorException
                        }
                    }
                }
            });
        }
    }
}
