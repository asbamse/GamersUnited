using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GamersUnited.Core.DomainService;
using GamersUnited.Core.Entities;
using GamersUnited.Infrastructure.Data.Context;

namespace GamersUnited.Infrastructure.Data
{
    public class GameGenreRepository : IRepository<GameGenre>
    {
        readonly GamersUnitedContext _ctx;

        public GameGenreRepository(GamersUnitedContext context)
        {
            _ctx = context;
        }

        public int Count()
        {
            return _ctx.GameGenre.Count();
        }

        public GameGenre Add(GameGenre obj)
        {
            if (obj.Name == null)
            {
                throw new ArgumentNullException("The name cannot be null");
            }

            var tmp = new GameGenre { Name = obj.Name };

            GameGenre item = _ctx.GameGenre.Add(tmp).Entity;
            _ctx.SaveChanges();

            return item;
        }

        public GameGenre GetById(int id)
        {
            var item = _ctx.GameGenre.FirstOrDefault(g => g.Id == id);

            if (item == null)
            {
                throw new ArgumentOutOfRangeException("Id not found!");
            }

            return item;
        }

        public IList<GameGenre> GetAll()
        {
            return _ctx.GameGenre.ToList();
        }

        public GameGenre Update(int id, GameGenre obj)
        {
            if (obj.Name == null)
            {
                throw new ArgumentNullException("The name cannot be null");
            }

            var item = GetById(id);
            item.Name = obj.Name;

            _ctx.GameGenre.Update(item);
            _ctx.SaveChanges();

            return item;
        }

        public GameGenre Remove(GameGenre obj)
        {
            var item = GetById(obj.Id);

            _ctx.GameGenre.Remove(item);
            _ctx.SaveChanges();

            return item;
        }
    }
}
