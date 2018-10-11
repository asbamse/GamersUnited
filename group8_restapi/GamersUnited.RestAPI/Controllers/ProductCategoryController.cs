using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamersUnited.Core.ApplicationService;
using GamersUnited.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GamersUnited.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        // GET: api/ProductCategory
        [HttpGet]
        public ActionResult<IEnumerable<ProductCategory>> Get()
        {
            try
            {
                return Ok(_productCategoryService.GetAll().AsEnumerable());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
