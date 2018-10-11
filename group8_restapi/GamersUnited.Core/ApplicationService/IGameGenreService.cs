﻿using GamersUnited.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamersUnited.Core.ApplicationService
{
    public interface IGameGenreService
    {
        List<GameGenre> GetGameGenres();
    }
}
