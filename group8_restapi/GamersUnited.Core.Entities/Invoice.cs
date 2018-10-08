using System;
using System.Collections.Generic;
using System.Text;

namespace GamersUnited.Core.Entities
{
    class Invoice
    {
        public int Id { get; set; }
        public Sold Product { get; set; }
        public User Customer { get; set; }
        public DateTime DateSold { get; set; }
    }
}
