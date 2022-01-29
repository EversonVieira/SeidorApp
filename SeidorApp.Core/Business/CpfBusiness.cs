using Microsoft.Extensions.Logging;
using SeidorApp.Core.Models;
using SeidorApp.Core.Repository;
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
        private readonly ILogger _logger;

        public CpfBusiness(IDBConnectionFactory connectionFactory, ILogger<CpfBusiness> logger, ILoggerFactory loggerFactory)
        {
            _cpfRepository = new CpfRepository(connectionFactory, new Logger<CpfRepository>(loggerFactory));
            _logger = logger;
        }

        public Response<long> Insert(Cpf cpf)
        {
            return _cpfRepository.Insert(cpf);
        }

        public Response<bool> Update(Cpf cpf)
        {
            return _cpfRepository.Update(cpf);
        }

        public Response<bool> Delete(Cpf cpf)
        {
            return _cpfRepository.Delete(cpf.Id);
        }
    }
}
