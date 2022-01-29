using SeidorCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeidorApp.Core.Models
{
    public class Cpf:BaseModel
    {
        public string? OwnerName { get; set; } 
        public string? Document { get; set; }
        public bool IsBlocked { get; set; }

    }
}
