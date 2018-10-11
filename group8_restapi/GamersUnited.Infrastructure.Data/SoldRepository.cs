using GamersUnited.Core.DomainService;
using GamersUnited.Core.Entities;
using GamersUnited.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

            var tmp = new Sold { ProductId = np.ProductId, Product = np, CDKey = obj.CDKey };

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
            var item = _ctx.Sold.FirstOrDefault(b => b.ProductId == id);

            if (item == null)
            {
                throw new ArgumentOutOfRangeException("Id not found!");
            }

            return item;
        }

        public Sold Remove(Sold obj)
        {
            var item = GetById(obj.ProductId);

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
            
            item = _ctx.Sold.Update(item).Entity;
            _ctx.SaveChanges();

            return item;
        }

        public IList<Sold> GetPage(PageProperty pageProperty)
        {
            if (pageProperty == null)
            {
                return GetAll();
            }

            IQueryable<Sold> quarySolds = _ctx.Sold;

            if (pageProperty.SortBy != null)
            {
                PropertyInfo propertyInfo = typeof(Sold).GetProperty(pageProperty.SortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo == null)
                {
                    throw new ArgumentException($"Cannot sort by {pageProperty.SortBy} because it is not a property in Sold! Try another.");
                }

                if (pageProperty.SortOrder == null || pageProperty.SortOrder.ToLower().Equals("asc"))
                {
                    quarySolds = quarySolds.OrderBy(p => propertyInfo.GetValue(p, null));
                }
                else if (pageProperty.SortOrder.ToLower().Equals("desc"))
                {
                    quarySolds = quarySolds.OrderByDescending(p => propertyInfo.GetValue(p, null));
                }
                else
                {
                    throw new ArgumentException($"Sort order can only be 'asc' or 'desc'! Not {pageProperty.SortOrder}.");
                }
            }

            List<Sold> sold = quarySolds
                .Skip((pageProperty.Page - 1) * pageProperty.Limit)
                .Take(pageProperty.Limit)
                .ToList();

            return sold;
        }
    }
}
