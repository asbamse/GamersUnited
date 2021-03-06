﻿using System;
using System.Collections.Generic;
using System.Text;
using GamersUnited.Core.Entities;

namespace GamersUnited.Core.ApplicationService
{
    public interface IGameService
    {
        List<Game> GetGames();
        Game AddGame(Game game);
        Game RemoveGame(Game game);
        Game UpdateGame(int id, Game game);
        Game GetGameById(int id);
    }
}
