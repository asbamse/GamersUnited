using System;
using System.Collections.Generic;
using System.Text;

namespace GamersUnited.Core.Entities
{
    public class Sold
    {
        public int SoldId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string CDKey { get; set; }
    }
}
