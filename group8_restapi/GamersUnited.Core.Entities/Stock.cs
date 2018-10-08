using System;
using System.Collections.Generic;
using System.Text;

namespace GamersUnited.Core.Entities
{
    public class Stock
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public string cdKey { get; set; }
    }
}
