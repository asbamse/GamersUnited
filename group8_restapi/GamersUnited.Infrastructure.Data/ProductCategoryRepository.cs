using GamersUnited.Core.DomainService;
using GamersUnited.Core.Entities;
using GamersUnited.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
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
            throw new NotImplementedException();
        }

        public ProductCategory Remove(ProductCategory obj)
        {
            throw new NotImplementedException();
        }

        public ProductCategory Update(int id, ProductCategory obj)
        {
            throw new NotImplementedException();
        }
    }
}
