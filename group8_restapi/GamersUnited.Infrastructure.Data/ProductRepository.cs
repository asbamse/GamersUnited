using GamersUnited.Core.DomainService;
using GamersUnited.Core.Entities;
using GamersUnited.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamersUnited.Infrastructure.Data
{
    public class ProductRepository : IRepository<Product>
    {
        readonly GamersUnitedContext _ctx;
        readonly IRepository<ProductCategory> _pcr;

        public ProductRepository(GamersUnitedContext context, IRepository<ProductCategory> productCategoryRepository)
        {
            _ctx = context;
            _pcr = productCategoryRepository;
        }

        public Product Add(Product obj)
        {
            if (obj.Name == null)
            {
                throw new ArgumentNullException("The name cannot be null");
            }
            else if (obj.Category == null)
            {
                throw new ArgumentNullException("The product category cannot be null");
            }
            else if (obj.ImageUrl == null)
            {
                throw new ArgumentNullException("The image url cannot be null");
            }
            else if (obj.Description == null)
            {
                throw new ArgumentNullException("The description cannot be null");
            }

            ProductCategory npc;
            if(obj.Category.Id > 0)
            {
                try
                {
                    npc = _pcr.GetById(obj.Category.Id);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    npc = _pcr.Add(obj.Category);
                }
            }
            else
            {
                npc = _pcr.Add(obj.Category);
            }

            var tmp = new Product { Name = obj.Name, Category = npc, Price = obj.Price, ImageUrl = obj.ImageUrl, Description = obj.Description };

            Product item = _ctx.Product.Add(tmp).Entity;
            _ctx.SaveChanges();

            return item;
        }

        public int Count()
        {
            return _ctx.Product.Count();
        }

        public IList<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Product Remove(Product obj)
        {
            throw new NotImplementedException();
        }

        public Product Update(int id, Product obj)
        {
            throw new NotImplementedException();
        }
    }
}
