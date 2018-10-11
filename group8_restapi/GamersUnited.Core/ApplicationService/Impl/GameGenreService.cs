using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GamersUnited.Core.DomainService;
using GamersUnited.Core.Entities;

namespace GamersUnited.Core.ApplicationService.Impl
{
    public class GameGenreService : IGameGenreService
    {
        private readonly IRepository<GameGenre> _gameGenreRepo;

        public GameGenreService(IRepository<GameGenre> gameGenreRepo)
        {
            _gameGenreRepo = gameGenreRepo;
        }

        public List<GameGenre> GetGameGenres()
        {
            var gameGenres = _gameGenreRepo.GetAll();
            return gameGenres.ToList();
        }
    }
}
