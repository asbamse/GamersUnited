using System;
using System.Collections.Generic;
using System.Text;
using GamersUnited.Core.DomainService;
using GamersUnited.Core.Entities;

namespace GamersUnited.Core.ApplicationService.Impl
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IRepository<ProductCategory> _pcr;

        public ProductCategoryService(IRepository<ProductCategory> productCategoryRepository)
        {
            _pcr = productCategoryRepository;
        }

        public IList<ProductCategory> GetAll()
        {
            return _pcr.GetAll();
        }
    }
}
