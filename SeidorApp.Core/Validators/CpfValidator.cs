using Microsoft.Extensions.Logging;
using SeidorApp.Core.Repository;
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

        

    }
}
