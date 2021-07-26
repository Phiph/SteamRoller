

using Dapr.Actors;
using SteamRoller.Core;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SteamRoller.Actors.Interface
{
    public interface IGameRoomActor : IActor
    {
        Task AddPlayer(Guid PlayerId);
        Task<Game> Rumble();

        Task<List<Guid>> GetPlayerList();

    }
}
