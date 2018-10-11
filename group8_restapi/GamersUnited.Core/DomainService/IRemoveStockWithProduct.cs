using GamersUnited.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamersUnited.Core.DomainService
{
    public interface IRemoveStockWithProduct
    {
        Stock RemoveFirstWithProduct(Product product);
    }
}
