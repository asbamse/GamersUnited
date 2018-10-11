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
    public class ProductCategoryRepository : IRepository<ProductCategory>
    {
        readonly GamersUnitedContext _ctx;

        public ProductCategoryRepository(GamersUnitedContext context)
        {
            _ctx = context;
        }

        public ProductCategory Add(ProductCategory obj)
        {
            if(obj.Name == null)
            {
                throw new ArgumentNullException("The name cannot be null");
            }

            var tmp = new ProductCategory { Name = obj.Name };

            ProductCategory item = _ctx.ProductCategory.Add(tmp).Entity;
            _ctx.SaveChanges();

            return item;
        }

        public int Count()
        {
            return _ctx.ProductCategory.Count();
        }

        public IList<ProductCategory> GetAll()
        {
            return _ctx.ProductCategory.ToList();
        }

        public ProductCategory GetById(int id)
        {
            var item = _ctx.ProductCategory.FirstOrDefault(b => b.ProductCategoryId == id);

            if (item == null)
            {
                throw new ArgumentOutOfRangeException("Id not found!");
            }

            return item;
        }

        public ProductCategory Remove(ProductCategory obj)
        {
            var item = GetById(obj.ProductCategoryId);

            _ctx.ProductCategory.Remove(item);
            _ctx.SaveChanges();

            return item;
        }

        public ProductCategory Update(int id, ProductCategory obj)
        {
            if (obj.Name == null)
            {
                throw new ArgumentNullException("The name cannot be null");
            }

            var item = GetById(id);
            item.Name = obj.Name;

            _ctx.ProductCategory.Update(item);
            _ctx.SaveChanges();

            return item;
        }

        public IList<ProductCategory> GetPage(PageProperty pageProperty)
        {
            if (pageProperty == null)
            {
                return GetAll();
            }

            IQueryable<ProductCategory> quaryProductCategorys = _ctx.ProductCategory;

            if (pageProperty.SortBy != null)
            {
                PropertyInfo propertyInfo = typeof(ProductCategory).GetProperty(pageProperty.SortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo == null)
                {
                    throw new ArgumentException($"Cannot sort by {pageProperty.SortBy} because it is not a property in ProductCategory! Try another.");
                }

                if (pageProperty.SortOrder == null || pageProperty.SortOrder.ToLower().Equals("asc"))
                {
                    quaryProductCategorys = quaryProductCategorys.OrderBy(p => propertyInfo.GetValue(p, null));
                }
                else if (pageProperty.SortOrder.ToLower().Equals("desc"))
                {
                    quaryProductCategorys = quaryProductCategorys.OrderByDescending(p => propertyInfo.GetValue(p, null));
                }
                else
                {
                    throw new ArgumentException($"Sort order can only be 'asc' or 'desc'! Not {pageProperty.SortOrder}.");
                }
            }

            List<ProductCategory> users = quaryProductCategorys
                .Skip((pageProperty.Page - 1) * pageProperty.Limit)
                .Take(pageProperty.Limit)
                .ToList();

            return users;
        }
    }
}
