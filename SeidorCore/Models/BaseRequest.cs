﻿using System.Collections.Generic;
using System.Net;

namespace SeidorCore.Models
{
    public class BaseRequest
    {
        public List<Filter> filters { get; set; } = new List<Filter>();
    }
}
