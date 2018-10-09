using System;
using System.Collections.Generic;
using System.Text;

namespace GamersUnited.Core.Entities
{
    public class Game : Product
    {
        public int ProductId { get; set; }
        public GameGenre Genre { get; set; }
    }
}
