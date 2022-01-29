using SeidorApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeidorApp.Core.Adapter.UserAdapter
{
    public class DTO_RegisterUser:User
    {
        public string? ConfirmPassword { get;set;}
    }
}
