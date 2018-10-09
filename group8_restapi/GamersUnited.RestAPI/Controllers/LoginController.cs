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
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        // POST: api/Login
        [HttpPost]
        public ActionResult<Boolean> Post([FromBody] User user)
        {
            try
            {
                return Ok(_loginService.ValidateLoginInformation(user.Email, user.Password));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
