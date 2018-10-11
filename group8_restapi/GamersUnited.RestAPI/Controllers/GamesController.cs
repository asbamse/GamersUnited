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
    public class GamesController : ControllerBase
    {
        private IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // GET: api/Games
        [HttpGet]
        public ActionResult<IEnumerable<Game>> Get()
        {
            try
            {
                return Ok(_gameService.GetGames().AsEnumerable());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/Games/filtered/
        [HttpGet("filtered/")]
        public ActionResult<IEnumerable<Game>> Get([FromHeader]PageProperty pageProperty)
        {
            try
            {
                return Ok(_gameService.GetPage(pageProperty).AsEnumerable());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/Games/count/
        [HttpGet("count/")]
        public ActionResult<int> Get([FromHeader]Random rand)
        {
            try
            {
                return Ok(_gameService.Count());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/Games/id/5
        [HttpGet("id/{id}")]
        public ActionResult<Game> Get(int id)
        {
            try
            {
                return Ok(_gameService.GetGameById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/Games
        [HttpPost]
        public ActionResult<Game> Post([FromBody] Game game)
        {
            try
            {
                return Ok(_gameService.AddGame(game));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Games/5
        [HttpPut("{id}")]
        public ActionResult<Game> Put(int id, [FromBody] Game game)
        {
            try
            {
                return Ok(_gameService.UpdateGame(id, game));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult<Game> Delete(int id)
        {
            try
            {
                return Ok(_gameService.RemoveGame(new Game() { GameId = id }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
