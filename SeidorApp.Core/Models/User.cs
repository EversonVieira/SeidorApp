using BaseCore.Interfaces;
using BaseCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeidorApp.Core.Models
{
    public class User:BaseModel, IUser
    {
        public string? Name { get; set; } 
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
