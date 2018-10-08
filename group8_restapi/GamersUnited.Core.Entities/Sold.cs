using System;
using System.Collections.Generic;
using System.Text;

namespace GamersUnited.Core.Entities
{
    public class Sold
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public string cdKey { get; set; }
    }
}
