﻿using GamersUnited.Core.DomainService;
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
    }
}
