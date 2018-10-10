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
    public class StocksController : ControllerBase
    {
        private IStockService _stockService;

        public StocksController(IStockService stockService)
        {
            _stockService = stockService;
        }

        // POST: api/Stocks
        [HttpPost]
        public ActionResult<int> Post([FromBody] Package package)
        {
            try
            {
                return Ok(_stockService.AddToStock(package.Amount, package.Product));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public class Package
        {
            public int Amount { get; set; }
            public Product Product { get; set; }
        }
    }
}
