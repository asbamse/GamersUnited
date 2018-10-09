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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            try
            {
                return Ok(_userService.GetAll().AsEnumerable());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<User> Get(int id)
        {
            try
            {
                return Ok(_userService.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/Users
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            try
            {
                return Ok(_userService.Add(user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public ActionResult<User> Put(int id, [FromBody] User user)
        {
            try
            {
                return Ok(_userService.Update(id, user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult<User> Delete(int id)
        {
            try
            {
                return Ok(_userService.Remove(new User() { Id=id }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
