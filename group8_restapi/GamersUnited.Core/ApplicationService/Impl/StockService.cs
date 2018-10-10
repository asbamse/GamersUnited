using System;
using System.Collections.Generic;
using System.Text;
using GamersUnited.Core.ApplicationService.Impl.Utilities;
using GamersUnited.Core.DomainService;
using GamersUnited.Core.Entities;

namespace GamersUnited.Core.ApplicationService.Impl
{
    public class StockService : IStockService
    {
        private readonly IRepository<Product> _pr;
        private readonly IRepository<Stock> _str;

        public StockService(IRepository<Product> productRepository, IRepository<Stock> stockRepository)
        {
            _pr = productRepository;
            _str = stockRepository;
        }

        public int AddToStock(int amount, Product product)
        {
            Requirement.NotNull(product, "Product");
            _pr.GetById(product.ProductId);

            Random rand = new Random();
            for (int i = 0; i < amount; i++)
            {
                _str.Add(new Stock { StockId=i, Product=new Product { ProductId = product.ProductId }, CDKey=Cryptography.Encrypt(rand.Next(1000).ToString()).Substring(47) });
            }

            return _str.Count();
        }
    }
}
