using GamersUnited.Core.DomainService;
using GamersUnited.Core.Entities;
using GamersUnited.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamersUnited.Infrastructure.Data
{
    public class StockRepository : IRepository<Stock>
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
            if(obj.Product.Id > 0)
            {
                try
                {
                    np = _pr.GetById(obj.Product.Id);
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

            var tmp = new Stock { Product = np, CDKey = obj.CDKey };

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
            var item = _ctx.Stock.FirstOrDefault(b => b.Id == id);

            if (item == null)
            {
                throw new ArgumentOutOfRangeException("Id not found!");
            }

            return item;
        }

        public Stock Remove(Stock obj)
        {
            var item = GetById(obj.Id);

            _ctx.Stock.Remove(item);
            _ctx.SaveChanges();

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
            if (obj.Product.Id > 0)
            {
                try
                {
                    np = _pr.GetById(obj.Product.Id);
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
            item.Product = np;
            item.CDKey = obj.CDKey;
            
            item = _ctx.Stock.Update(item).Entity;
            _ctx.SaveChanges();

            return item;
        }
    }
}
