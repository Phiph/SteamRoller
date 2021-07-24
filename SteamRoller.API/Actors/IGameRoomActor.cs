using Dapr.Actors;
using SteamRoller.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamRoller.API.Actors
{
    public interface IGameRoomActor : IActor
    {
        Task AddPlayer(Guid PlayerId);
        Task<Game> Rumble();

    }
}
