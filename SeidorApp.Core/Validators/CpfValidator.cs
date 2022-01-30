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
    internal class CpfValidator:BaseValidator
    {
        private readonly CpfRepository _cpfRepository;
        private readonly ILogger _logger;

        public CpfValidator(CpfRepository cpfRepository, ILogger<CpfValidator> logger)
        {
            _cpfRepository = cpfRepository;
            _logger = logger;
        }

		public void ValidateInsert(BaseResponse response, Cpf model, bool cumulativeMessages = true)
        {
			CommonValidation(response, model, cumulativeMessages);
        }

		public void ValidateUpdate(BaseResponse response, Cpf model, bool cumulativeMessages = true)
		{
			if (model.Id < 1)
			{
				response.AddValidationMessage("002", string.Format(REQUIRED_WITH_ARGUMENT, "Id"));
				if (!cumulativeMessages) return;
			}

			CommonValidation(response, model, cumulativeMessages);
		}

		public void ValidateDelete(BaseResponse response, Cpf model)
		{
			if (model.Id < 1)
			{
				response.AddValidationMessage("002", string.Format(REQUIRED_WITH_ARGUMENT, "Id"));
			}
		}

		private void CommonValidation(BaseResponse response, Cpf model, bool cumulativeMessages = true)
        {
            if (model.OwnerName.IsNullOrEmpty())
            {
				response.AddValidationMessage("002", "O campo 'Nome da pessoa' é obrigatório.");
				if (!cumulativeMessages) return;
            }

			if (!ValidateCpfMask(model.Document.Trim()))
            {
				response.AddValidationMessage("002", "O CPF informado não está no formato correto.");
				if (!cumulativeMessages) return;
            }

			if (!IsValidCpf(model.Document.Trim()))
			{
				response.AddValidationMessage("002", "O CPF informado não é válido.");
				if (!cumulativeMessages) return;
			}

		}

		private bool ValidateCpfMask(string cpf)
        {
			//000.000.000-00
			return cpf.Trim().Length == 14 &&
				   cpf[3] == '.' &&
				   cpf[7] == '.' &&
				   cpf[11] == '-';
        }
        private bool IsValidCpf(string cpf)
        {
			int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			string tempCpf;
			string digit;
			int sum;
			int rest;
			cpf = cpf.Trim();
			cpf = cpf.Replace(".", "").Replace("-", "");
			if (cpf.Length != 11)
				return false;
			tempCpf = cpf.Substring(0, 9);
			sum = 0;

			for (int i = 0; i < 9; i++)
				sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];
			rest = sum % 11;
			if (rest < 2)
				rest = 0;
			else
				rest = 11 - rest;
			digit = rest.ToString();
			tempCpf = tempCpf + digit;
			sum = 0;
			for (int i = 0; i < 10; i++)
				sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];
			rest = sum % 11;
			if (rest < 2)
				rest = 0;
			else
				rest = 11 - rest;

			digit = digit + rest.ToString();

			return cpf.EndsWith(digit);

		}
        

    }
}
