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
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        // GET: api/Invoices
        [HttpGet]
        public ActionResult<IEnumerable<Invoice>> Get()
        {
            try
            {
                return Ok(_invoiceService.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/Invoices/5
        [HttpGet("{id}")]
        public ActionResult<Invoice> Get(int id)
        {
            try
            {
                return Ok(_invoiceService.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/Invoices
        [HttpPost]
        public ActionResult<Invoice> Post([FromBody] Package package)
        {
            try
            {
                return Ok(_invoiceService.Buy(package.Customer, package.Products, package.SoldDate));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public class Package
        {
            public User Customer { get; set; }
            public IList<Product> Products { get; set; }
            public DateTime SoldDate { get; set; }
        }
    }
}
