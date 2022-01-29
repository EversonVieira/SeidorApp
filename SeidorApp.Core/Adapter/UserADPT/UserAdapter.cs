using Microsoft.Extensions.Logging;
using SeidorApp.Core.Adapter.UserAdapter;
using SeidorApp.Core.Business;
using SeidorApp.Core.Utility;
using SeidorCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeidorApp.Core.Adapt
{
    public class UserAdapter
    {
        private readonly ILogger _logger;
        private readonly UserBusiness _userBusiness;

        public UserAdapter(ILogger<UserAdapter> logger, UserBusiness userBusiness)
        {
            this._logger = logger;
            this._userBusiness = userBusiness;
        }

        public Response<long> Insert(DTO_RegisterUser user)
        {
            Response<long> response = new Response<long>();

            if(user.Password != user.ConfirmPassword)
            {
                response.AddValidationMessage("003", "As senhas não são iguais.");
                return response;
            }

            response = _userBusiness.Insert(user);

            return response;
        }

        public Response<bool> Update(DTO_RegisterUser user)
        {
            Response<bool> response = new Response<bool>();

            if (user.Password != user.ConfirmPassword)
            {
                response.AddValidationMessage("003", "As senhas não são iguais.");
                return response;
            }

            response = _userBusiness.Update(user);

            return response;
        }
    }
}
