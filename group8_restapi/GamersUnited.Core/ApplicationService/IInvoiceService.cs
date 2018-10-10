using GamersUnited.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamersUnited.Core.ApplicationService
{
    public interface IInvoiceService
    {
        Invoice Buy(User customer, IList<Product> products, DateTime soldDate);
        Invoice GetById(int id);
        IList<Invoice> GetAll();
    }
}
