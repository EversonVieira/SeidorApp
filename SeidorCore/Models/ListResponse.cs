using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeidorCore.Models
{
    public class ListResponse<T>:Response<List<T>>
    {
        public int TotalItemsOnPage { get; set; }
        public int PageIndex { get; set; }
        public int TotalItems { get; set; }
    }
}
