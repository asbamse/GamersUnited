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
    public class GameGenresController : ControllerBase
    {
        private readonly IGameGenreService _productCategoryService;

        public GameGenresController(IGameGenreService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        // GET: api/GameGenres
        [HttpGet]
        public ActionResult<IEnumerable<GameGenre>> Get()
        {
            try
            {
                return Ok(_productCategoryService.GetGameGenres().AsEnumerable());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
