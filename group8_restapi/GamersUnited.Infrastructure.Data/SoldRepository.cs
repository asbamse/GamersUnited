using GamersUnited.Core.DomainService;
using GamersUnited.Core.Entities;
using GamersUnited.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamersUnited.Infrastructure.Data
{
    public class SoldRepository : IRepository<Sold>
    {
        readonly GamersUnitedContext _ctx;
        readonly IRepository<Product> _pr;

        public SoldRepository(GamersUnitedContext context, IRepository<Product> productRepository)
        {
            _ctx = context;
            _pr = productRepository;
        }

        public Sold Add(Sold obj)
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

            var tmp = new Sold { Product = np, CDKey = obj.CDKey };

            Sold item = _ctx.Sold.Add(tmp).Entity;
            _ctx.SaveChanges();

            return item;
        }

        public int Count()
        {
            return _ctx.Sold.Count();
        }

        public IList<Sold> GetAll()
        {
            return _ctx.Sold.ToList();
        }

        public Sold GetById(int id)
        {
            var item = _ctx.Sold.FirstOrDefault(b => b.Id == id);

            if (item == null)
            {
                throw new ArgumentOutOfRangeException("Id not found!");
            }

            return item;
        }

        public Sold Remove(Sold obj)
        {
            var item = GetById(obj.Id);

            _ctx.Sold.Remove(item);
            _ctx.SaveChanges();

            return item;
        }

        public Sold Update(int id, Sold obj)
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
            
            item = _ctx.Sold.Update(item).Entity;
            _ctx.SaveChanges();

            return item;
        }
    }
}
