using System;
using System.Collections.Generic;
using System.Text;

namespace GamersUnited.Core.Entities
{
    public class PageProperty
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
    }
}
