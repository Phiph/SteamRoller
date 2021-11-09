

using Dapr.Actors;
using SteamRoller.Core;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SteamRoller.Actors.Interface
{
    public interface IPlayerActor : IActor
    {
        Task UploadLibrary(SteamLibrary library);

        Task<List<Game>> ReadyToPlayGames();

        Task<List<Game>> FilterGamesBy(StateFlags steamState);
    } 
}
