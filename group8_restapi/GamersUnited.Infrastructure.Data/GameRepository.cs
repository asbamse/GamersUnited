using GamersUnited.Core.DomainService;
using GamersUnited.Core.Entities;
using GamersUnited.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamersUnited.Infrastructure.Data
{
    public class GameRepository : IRepository<Game>
    {
        readonly GamersUnitedContext _ctx;
        readonly IRepository<Product> _pr;
        readonly IRepository<GameGenre> _ggr;

        public GameRepository(GamersUnitedContext context, IRepository<Product> productRepository, IRepository<GameGenre> gameGenreRepository)
        {
            _ctx = context;
            _pr = productRepository;
            _ggr = gameGenreRepository;
        }

        public Game Add(Game obj)
        {
            if (obj.Product == null)
            {
                throw new ArgumentNullException("The product cannot be null");
            }
            else if (obj.Genre == null)
            {
                throw new ArgumentNullException("The game genre cannot be null");
            }

            Product np;
            if(obj.Product.ProductId > 0)
            {
                try
                {
                    np = _pr.GetById(obj.Product.ProductId);
                }
                catch (ArgumentOutOfRangeException)
                {
                    np = _pr.Add(obj.Product);
                }
            }
            else
            {
                np = _pr.Add(obj.Product);
            }

            GameGenre ngg;
            if (obj.Genre.GameGenreId > 0)
            {
                try
                {
                    ngg = _ggr.GetById(obj.Genre.GameGenreId);
                }
                catch (ArgumentOutOfRangeException)
                {
                    ngg = _ggr.Add(obj.Genre);
                }
            }
            else
            {
                ngg = _ggr.Add(obj.Genre);
            }

            _ctx.SaveChanges();
            var tmp = new Game { Product = np, Genre = ngg };

            Game item = _ctx.Game.Add(tmp).Entity;
            _ctx.SaveChanges();

            return item;
        }

        public int Count()
        {
            return _ctx.Game.Count();
        }

        public IList<Game> GetAll()
        {
            return _ctx.Game.Include(g => g.Product).ThenInclude(p => p.Category).Include(g => g.Genre).ToList();
        }

        public Game GetById(int id)
        {
            var item = _ctx.Game.FirstOrDefault(b => b.GameId == id);

            if (item == null)
            {
                throw new ArgumentOutOfRangeException("Id not found!");
            }

            return item;
        }

        public Game Remove(Game obj)
        {
            var item = GetById(obj.GameId);

            _ctx.Game.Remove(item);
            _ctx.SaveChanges();

            return item;
        }

        public Game Update(int id, Game obj)
        {
            if (obj.Product == null)
            {
                throw new ArgumentNullException("The product cannot be null");
            }
            else if (obj.Genre == null)
            {
                throw new ArgumentNullException("The game genre cannot be null");
            }

            Product np;
            if (obj.Product.ProductId > 0)
            {
                try
                {
                    _pr.GetById(obj.Product.ProductId);
                    np = _pr.Update(obj.Product.ProductId, obj.Product);
                }
                catch (ArgumentOutOfRangeException)
                {
                    np = _pr.Add(obj.Product);
                }
            }
            else
            {
                np = _pr.Add(obj.Product);
            }

            GameGenre ngg;
            if (obj.Genre.GameGenreId > 0)
            {
                try
                {
                    ngg = _ggr.GetById(obj.Genre.GameGenreId);
                }
                catch (ArgumentOutOfRangeException)
                {
                    ngg = _ggr.Add(obj.Genre);
                }
            }
            else
            {
                ngg = _ggr.Add(obj.Genre);
            }

            var item = GetById(id);
            item.Product = np;
            item.Genre = ngg;
            
            item = _ctx.Game.Update(item).Entity;
            _ctx.SaveChanges();

            return item;
        }
    }
}
