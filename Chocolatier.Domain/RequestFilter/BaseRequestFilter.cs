﻿using System.ComponentModel;

namespace Chocolatier.Domain.RequestFilter
{
    public class BaseRequestFilter
    {
        [DefaultValue(1)]
        public int CurrentPage { get; set; }
        [DefaultValue(12)]
        public int PageSize { get; set; }
    }
}
