using Microsoft.AspNetCore.Mvc;
using SeidorApp.Core.Adapt;
using SeidorApp.Core.Adapter.UserAdapter;
using SeidorApp.Core.Business;
using BaseCore.ApiControllers;
using BaseCore.Models;
using BaseCore.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SeidorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : NDBaseController
    {

        private readonly UserBusiness _userBusiness;
        private readonly UserAdapter _userAdapter;
        private readonly ILogger _logger;

        public UserController(UserBusiness userBusiness, UserAdapter userAdapter, ILogger<UserController> logger, IAuth authService):base(logger,authService)
        {
            _userBusiness = userBusiness;
            _userAdapter = userAdapter;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult<Response<long>> Insert(DTO_RegisterUser user)
        {
            try
            {
                return GetResponse(() => _userAdapter.Insert(user));
            }
            catch(Exception ex)
            {
                return GetErrorResponse<long>(ex);
            }
        }

        [HttpPut]
        public ActionResult<Response<bool>> Update(DTO_RegisterUser user)
        {
            try
            {
                return GetResponse(() => _userAdapter.Update(user));
            }
            catch (Exception ex)
            {
                return GetErrorResponse<bool>(ex);
            }
        }
    }
}
