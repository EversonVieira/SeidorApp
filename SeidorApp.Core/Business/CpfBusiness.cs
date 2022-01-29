using Microsoft.Extensions.Logging;
using SeidorApp.Core.Models;
using SeidorApp.Core.Repository;
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

            _cpfValidator.ValidateUpdate(response, cpf);
            if (response.HasValidationMessages)
                return response;

            response =  _cpfRepository.Update(cpf);
            _cpfRepository.CloseConnection();

            return response;
        }

        public Response<bool> Delete(Cpf cpf)
        {
            Response<bool> response = new Response<bool>();

            _cpfValidator.ValidateDelete(response, cpf);
            if (response.HasValidationMessages)
                return response;

            response = _cpfRepository.Delete(cpf.Id);
            _cpfRepository.CloseConnection();

            return response;
        }
    }
}
