using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeidorApp.Core.Models
{
    public class Session
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string? Key { get; set; }
        public DateTime LastUse { get; set; }
    }
}
