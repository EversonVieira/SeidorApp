using BaseCore.ApiControllers;
using BaseCore.Interfaces;
using BaseCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeidorApp.Core.Models;

namespace SeidorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : NDBaseController
    {
        private readonly ILogger _logger;

        public LoginController(ILogger<LoginController> logger, IAuth authService) : base(logger, authService)
        {
            _logger = logger;
        }

        [HttpPost("login")]
        public ActionResult<Response<string>> DoLogin(User user)
        {
            try
            {
                return GetResponse(() => _authService.DoLogin(user),requireAuthentication:false);
            }
            catch(Exception ex)
            {
                return GetErrorResponse<string>(ex);
            }
        }

        [HttpGet("logout")]
        public ActionResult<Response<bool>> DoLogout()
        {
            try
            {
                return GetResponse(() => _authService.DoLogout());
            }
            catch (Exception ex)
            {
                return GetErrorResponse<bool>(ex);
            }
        }

        [HttpGet("validateSession")]
        public ActionResult<Response<IUser>> ValidateSession()
        {
            try
            {
                return GetResponse(() => _authService.ValidateSession(), requireAuthentication:false);
            }
            catch(Exception ex)
            {
                return GetErrorResponse<IUser>(ex);
            }
        } 
    }
}
