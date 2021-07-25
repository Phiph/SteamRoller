

using Dapr.Actors;
using SteamRoller.Core;
using System;
using System.Threading.Tasks;

namespace SteamRoller.Actors.Interface
{
    public interface IGameRoomActor : IActor
    {
        Task AddPlayer(Guid PlayerId);
        Task<Game> Rumble();

    }
}
