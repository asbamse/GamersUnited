using GamersUnited.Core.DomainService;
using GamersUnited.Core.Entities;
using GamersUnited.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
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
            ProductCategory item = _ctx.ProductCategory.Add(obj).Entity;
            _ctx.SaveChanges();

            return item;
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public IList<ProductCategory> GetAll()
        {
            throw new NotImplementedException();
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
