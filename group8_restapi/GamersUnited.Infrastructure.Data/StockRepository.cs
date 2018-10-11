using GamersUnited.Core.DomainService;
using GamersUnited.Core.Entities;
using GamersUnited.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GamersUnited.Infrastructure.Data
{
    public class StockRepository : IRepository<Stock>, IRemoveStockWithProduct
    {
        readonly GamersUnitedContext _ctx;
        readonly IRepository<Product> _pr;

        public StockRepository(GamersUnitedContext context, IRepository<Product> productRepository)
        {
            _ctx = context;
            _pr = productRepository;
        }

        public Stock Add(Stock obj)
        {
            if (obj.Product == null)
            {
                throw new ArgumentNullException("The product cannot be null");
            }
            else if (obj.CDKey == null)
            {
                throw new ArgumentNullException("The cd key cannot be null");
            }

            Product np;
            if(obj.Product.ProductId > 0)
            {
                try
                {
                    np = _pr.GetById(obj.Product.ProductId);
                }
                catch (ArgumentOutOfRangeException)
                {
                    np = _pr.Add(obj.Product);
                }
            }
            else
            {
                np = _pr.Add(obj.Product);
            }

            var tmp = new Stock { ProductId = np.ProductId, Product = np, CDKey = obj.CDKey };

            Stock item = _ctx.Stock.Add(tmp).Entity;
            _ctx.SaveChanges();

            return item;
        }

        public int Count()
        {
            return _ctx.Stock.Count();
        }

        public IList<Stock> GetAll()
        {
            return _ctx.Stock.ToList();
        }

        public Stock GetById(int id)
        {
            var item = _ctx.Stock.FirstOrDefault(b => b.StockId == id);

            if (item == null)
            {
                throw new ArgumentOutOfRangeException("Id not found!");
            }

            return item;
        }

        public Stock Remove(Stock obj)
        {
            var item = GetById(obj.StockId);

            _ctx.Stock.Remove(item);
            _ctx.SaveChanges();

            return item;
        }

        public Stock RemoveFirstWithProduct(Product product)
        {
            var item = _ctx.Stock.Include(s => s.Product).FirstOrDefault(b => b.Product.ProductId == product.ProductId);

            if (item == null)
            {
                throw new ArgumentException("Product not in stock!");
            }
            Remove(item);

            return item;
        }

        public Stock Update(int id, Stock obj)
        {
            if (obj.Product == null)
            {
                throw new ArgumentNullException("The product cannot be null");
            }
            else if (obj.CDKey == null)
            {
                throw new ArgumentNullException("The cd key cannot be null");
            }

            Product np;
            if (obj.Product.ProductId > 0)
            {
                try
                {
                    np = _pr.GetById(obj.Product.ProductId);
                }
                catch (ArgumentOutOfRangeException)
                {
                    np = _pr.Add(obj.Product);
                }
            }
            else
            {
                np = _pr.Add(obj.Product);
            }

            var item = GetById(id);
            item.ProductId = np.ProductId;
            item.Product = np;
            item.CDKey = obj.CDKey;
            
            item = _ctx.Stock.Update(item).Entity;
            _ctx.SaveChanges();

            return item;
        }

        public IList<Stock> GetPage(PageProperty pageProperty)
        {
            if (pageProperty == null)
            {
                return GetAll();
            }

            IQueryable<Stock> quaryStocks = _ctx.Stock;

            if (pageProperty.SortBy != null)
            {
                PropertyInfo propertyInfo = typeof(Stock).GetProperty(pageProperty.SortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo == null)
                {
                    throw new ArgumentException($"Cannot sort by {pageProperty.SortBy} because it is not a property in Stock! Try another.");
                }

                if (pageProperty.SortOrder == null || pageProperty.SortOrder.ToLower().Equals("asc"))
                {
                    quaryStocks = quaryStocks.OrderBy(p => propertyInfo.GetValue(p, null));
                }
                else if (pageProperty.SortOrder.ToLower().Equals("desc"))
                {
                    quaryStocks = quaryStocks.OrderByDescending(p => propertyInfo.GetValue(p, null));
                }
                else
                {
                    throw new ArgumentException($"Sort order can only be 'asc' or 'desc'! Not {pageProperty.SortOrder}.");
                }
            }

            List<Stock> stocks = quaryStocks
                .Skip((pageProperty.Page - 1) * pageProperty.Limit)
                .Take(pageProperty.Limit)
                .ToList();

            return stocks;
        }
    }
}
