using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeidorApp.Core.Validators
{
    public  class BaseValidator
    {
        public const string REQUIRED_WITH_ARGUMENT = "O campo '{0}' é obrigatório.";
        public const string INVALID_ARGUMENT = "O valor '{0}' não atende aos requisitos de validação do campo '{1}'.";
        public const string MIN_LENGTH = "'{0}' deve ter um tamanho mínimo de '{1}' caracteres";
    }
}
