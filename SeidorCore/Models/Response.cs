using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCore.Models
{
    public class Response<T>:BaseResponse
    {
        public T? Data { get; set; }
    }
}
