using Microsoft.Extensions.Logging;
using SeidorApp.Core.Adapter.UserAdapter;
using SeidorApp.Core.Business;
using SeidorApp.Core.Utility;
using BaseCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCore.Extensions;
using SeidorApp.Core.Models;

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
            if (user.IsNull())
            {
                throw new InvalidOperationException("Cpf não pode ser vazio");
            }

            if (user.Password != user.ConfirmPassword)
            {
                response.AddValidationMessage("003", "As senhas não são iguais.");
                return response;
            }

            response = _userBusiness.Insert(user);

            return response;
        }

        public Response<bool> Update(DTO_UpdateUser user)
        {
            Response<bool> response = new Response<bool>();
            if (user.IsNull())
            {
                throw new InvalidOperationException("Cpf não pode ser vazio");
            }

            // Cria o filtro de Login para verificar se a senha antiga foi informada corretamente
            Request validationRequest = new Request();
            validationRequest.filters.AddRange(new List<Filter>()
            {
                new Filter
                {
                    Target1 = nameof(DTO_UpdateUser.Email),
                    OperationType = FilterOperationType.Equals,
                    Value1 = user.Email,
                },
                new Filter
                {
                    Target1 = nameof(DTO_UpdateUser.Password),
                    OperationType = FilterOperationType.Equals,
                    Value1 = LoginUtility.EncryptPassword(user.OldPassword),
                },
            });

            ListResponse<User> userResponse = _userBusiness.FindUserByRequest(validationRequest);
            if (userResponse.HasAnyMessages)
            {
                response.Merge(userResponse);
                return response;
            }

            if(!userResponse.HasResponseData || !userResponse.Data.Any())
            {
                response.AddValidationMessage("003", "Senha atual incorreta.");
                return response;
            }
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
