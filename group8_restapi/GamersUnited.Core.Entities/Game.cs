using System;
using System.Collections.Generic;
using System.Text;

namespace GamersUnited.Core.Entities
{
    class Game
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public GameGenre Genre { get; set; }
    }
}
