using System;
using System.Collections.Generic;
using System.Text;
using GamersUnited.Core.ApplicationService.Impl.Utilities;
using GamersUnited.Core.DomainService;
using GamersUnited.Core.Entities;

namespace GamersUnited.Core.ApplicationService.Impl
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IRepository<Invoice> _ir;
        private readonly IRepository<User> _ur;
        private readonly IRepository<Stock> _str;
        private readonly IRemoveStockWithProduct _srtr;
        private readonly IRepository<Sold> _sor;

        public InvoiceService(IRepository<Invoice> invoiceRepository, IRepository<User> userRepository, IRepository<Stock> stockRepository, IRemoveStockWithProduct stockRemoveRepository, IRepository<Sold> soldRepository)
        {
            _ir = invoiceRepository;
            _ur = userRepository;
            _str = stockRepository;
            _srtr = stockRemoveRepository;
            _sor = soldRepository;
        }

        public Invoice Buy(User customer, IList<Product> products, DateTime soldDate)
        {
            Requirement.NotNull(customer, "Customer");
            Requirement.MinLength(1, customer.UserId, "Customer ID");
            customer = _ur.GetById(customer.UserId);
            Requirement.NotNull(products, "Products");

            List<Stock> stockProducts = new List<Stock>();
            try
            {
                for (int i = 0; i < products.Count; i++)
                {
                    Requirement.NotNull(products[i], "Product");
                    var stock = _srtr.RemoveFirstWithProduct(products[i]);

                    stockProducts.Add(stock);
                }
            }
            catch (Exception e)
            {
                for (int i = 0; i < stockProducts.Count; i++)
                {
                    _str.Add(stockProducts[i]);
                }

                throw e;
            }

            List<SoldInvoiceRelation> soldProducts = new List<SoldInvoiceRelation>();
            for (int i = 0; i < stockProducts.Count; i++)
            {
                soldProducts.Add(new SoldInvoiceRelation { Sold = _sor.Add(new Sold { Product = stockProducts[i].Product, CDKey = stockProducts[i].CDKey }) });
            }

            return _ir.Add(new Invoice { Customer=customer, Products=soldProducts, DateSold=soldDate });
        }

        public IList<Invoice> GetAll()
        {
            return _ir.GetAll();
        }

        public Invoice GetById(int id)
        {
            return _ir.GetById(id);
        }
    }
}
