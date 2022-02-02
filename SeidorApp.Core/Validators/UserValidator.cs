using Microsoft.Extensions.Logging;
using SeidorApp.Core.Models;
using SeidorApp.Core.Repository;
using BaseCore.Extensions;
using BaseCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeidorApp.Core.Validators
{
    internal class UserValidator : BaseValidator
    {
        private readonly ILogger _logger;
        private readonly UserRepository _userRepository;

        public UserValidator(UserRepository userRepository, ILogger<UserValidator> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public void ValidateInsert(BaseResponse response, User model, bool cumulativeValidations = true)
        {
            
            CommonValidate(response, model, cumulativeValidations);
            if (!cumulativeValidations) return;

            ValidateEmailInUse(response, model, false);
        }

        public void ValidateUpdate(BaseResponse response, User model, bool cumulativeValidations = true)
        {
            if (model.Id < 1)
            {
                response.AddValidationMessage("002", string.Format(REQUIRED_WITH_ARGUMENT, "Id"));
                if (!cumulativeValidations) return;

            }

            CommonValidate(response, model, cumulativeValidations);
            if (!cumulativeValidations) return;

            ValidateEmailInUse(response, model, true);

        }

        public void ValidateDelete(BaseResponse response, User model)
        {
            if (model.Id < 1)
            {
                response.AddValidationMessage("002", string.Format(REQUIRED_WITH_ARGUMENT, "Id"));
            }
        }

        private void CommonValidate(BaseResponse response, User model, bool cumulativeValidations = true)
        {
            if (model.Name.IsNullOrEmpty())
            {
                response.AddValidationMessage("002", string.Format(REQUIRED_WITH_ARGUMENT, "Nome"));
                if (!cumulativeValidations) return;
            }

            if (model.Email.IsNullOrEmpty())
            {
                response.AddValidationMessage("002", string.Format(REQUIRED_WITH_ARGUMENT, "E-mail"));
                if (!cumulativeValidations) return;
            }

            if (!model.Email.Contains("@"))
            {
                response.AddValidationMessage("002", string.Format(INVALID_ARGUMENT, model.Email, "E-mail"));
                if (!cumulativeValidations) return;
            }

            if (model.Password.IsNullOrEmpty())
            {
                response.AddValidationMessage("002", string.Format(REQUIRED_WITH_ARGUMENT, "Senha"));
                if (!cumulativeValidations) return;
            }

            if (model.Password.Length < 6)
            {
                response.AddValidationMessage("002", string.Format(MIN_LENGTH, "Senha", 6));
                if (!cumulativeValidations) return;
            }

        }

        private void ValidateEmailInUse(BaseResponse response, User model, bool verifyId)
        {
            Request request = new Request();
            request.filters.Add(new Filter()
            {
                Target1 = "Email",
                Value1 = model.Email,
                OperationType = FilterOperationType.Equals
            });

            ListResponse<User> userResponse = _userRepository.FindByRequest(request);
            if(userResponse.Data.IsNotNull() && userResponse.Data.Any() && (!verifyId || userResponse.Data.First().Id != model.Id ))
            {
                response.AddValidationMessage("003", "E-mail já está em uso por outro usuário.");
            }
        }
    }
}
