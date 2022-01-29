using BaseCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCore.Interfaces
{
    public interface IAuth
    {
        public Response<string> DoLogin(IUser user);
        public Response<bool> DoLogout();
        public Response<bool> ValidateSession();
       
    }
}
