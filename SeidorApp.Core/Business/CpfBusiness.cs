using Microsoft.Extensions.Logging;
using SeidorApp.Core.Models;
using SeidorApp.Core.Repository;
using SeidorApp.Core.Validators;
using BaseCore.DataBase.Factory;
using BaseCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCore.Extensions;

namespace SeidorApp.Core.Business
{
    public class CpfBusiness
    {
        private readonly CpfRepository _cpfRepository;
        private readonly CpfValidator _cpfValidator;
        private readonly ILogger _logger;

        public CpfBusiness(IDBConnectionFactory connectionFactory, ILogger<CpfBusiness> logger, ILoggerFactory loggerFactory)
        {
            _cpfRepository = new CpfRepository(connectionFactory, new Logger<CpfRepository>(loggerFactory));
            _cpfValidator = new CpfValidator(_cpfRepository, new Logger<CpfValidator>(loggerFactory));
            _logger = logger;
        }

        public Response<long> Insert(Cpf cpf)
        {
            Response<long> response = new Response<long>();
            if (cpf.IsNull())
            {
                throw new InvalidOperationException("Cpf não pode ser vazio"); 
            }

            _cpfValidator.ValidateInsert(response, cpf);
            if (response.HasValidationMessages)
                return response;


            response = _cpfRepository.Insert(cpf);
            _cpfRepository.CloseConnection();

            return response;
        }

        public Response<bool> Update(Cpf cpf)
        {
            Response<bool> response = new Response<bool>();
            if (cpf.IsNull())
            {
                throw new InvalidOperationException("Cpf não pode ser vazio");
            }

            _cpfValidator.ValidateUpdate(response, cpf);
            if (response.HasValidationMessages)
                return response;

            response =  _cpfRepository.Update(cpf);
            _cpfRepository.CloseConnection();

            return response;
        }

        public ListResponse<Cpf> FindByRequest(Request request)
        {
            if(request.IsNull())
            {
                throw new InvalidOperationException("Request não pode ser vazio");
            }
            return _cpfRepository.FindByRequest(request);
        }

        public Response<int> FindCountByRequest(Request request)
        {
            if (request.IsNull())
            {
                throw new InvalidOperationException("Request não pode ser vazio");
            }
            return _cpfRepository.FindCountByRequest(request);
        }
        public ListResponse<Cpf> FindByDocument(string document)
        {
            ListResponse<Cpf> response = new ListResponse<Cpf>();
            if (document.IsNull())
            {
                response.AddValidationMessage("002", "Informe um cpf para buscar.");
                return response;
            }

            if (!_cpfValidator.ValidateCpfMask(document.Trim()))
            {
                response.AddValidationMessage("002", "O CPF informado não está no formato correto.");
            }

            if (!_cpfValidator.IsValidCpf(document.Trim()))
            {
                response.AddValidationMessage("002", "O CPF informado não é válido.");
            }

            if (response.HasValidationMessages)
            {
                return response;
            }


            Request request = new Request();
            request.filters.Add(new Filter
            {
                Target1 = nameof(Cpf.Document),
                OperationType = FilterOperationType.Like,
                Value1 = $"%{document}%"
            });

            response = _cpfRepository.FindByRequest(request);
            return response;
        }

        public ListResponse<Cpf> FindByBlockStatus(bool isBlocked)
        {
            ListResponse<Cpf> response = new ListResponse<Cpf>();

            Request request = new Request();
            request.filters.Add(new Filter
            {
                Target1 = nameof(Cpf.IsBlocked),
                OperationType = FilterOperationType.Equals,
                Value1 = isBlocked
            });

            response = _cpfRepository.FindByRequest(request);
            return response;
        } 
        public Response<bool> Delete(Cpf cpf)
        {
            Response<bool> response = new Response<bool>();
            if (cpf.IsNull())
            {
                throw new InvalidOperationException("Cpf não pode ser vazio");
            }

            _cpfValidator.ValidateDelete(response, cpf);
            if (response.HasValidationMessages)
                return response;

            response = _cpfRepository.Delete(cpf.Id);
            _cpfRepository.CloseConnection();

            return response;
        }
    }
}
