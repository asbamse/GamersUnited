using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GamersUnited.Core.DomainService;
using GamersUnited.Core.Entities;

namespace GamersUnited.Core.ApplicationService.Impl
{
    public class GameService : IGameService
    {
        private readonly IRepository<Game> _gameRepo;

        public GameService(IRepository<Game> gameRepo)
        {
            _gameRepo = gameRepo;
        }

        public List<Game> GetGames()
        {
            var games = _gameRepo.GetAll();
            return games.ToList();
        }

        public Game AddGame(Game game)
        {
            return _gameRepo.Add(game);
        }

        public Game RemoveGame(Game game)
        {
            return _gameRepo.Remove(game);
        }

        public Game UpdateGame(int id, Game game)
        {
            return _gameRepo.Update(id, game);
        }

        public Game GetGameById(int id)
        {
            return _gameRepo.GetById(id);
        }
    }
}
