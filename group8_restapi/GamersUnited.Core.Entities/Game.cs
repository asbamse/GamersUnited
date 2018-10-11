using System;
using System.Collections.Generic;
using System.Text;

namespace GamersUnited.Core.Entities
{
    public class Game
    {
        public int GameId { get; set; }
        public int ProductFK { get; set; }
        public Product Product { get; set; }
        public GameGenre Genre { get; set; }
    }
}
