using System;
using System.Collections.Generic;
using System.Text;

namespace GamersUnited.Core.Entities
{
    public class SoldInvoiceRelation
    {
        public int SoldId { get; set; }
        public int ProductId { get; set; }
        public Sold Sold { get; set; }

        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}
