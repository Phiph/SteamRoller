using Dapr.Actors.Runtime;
using SteamRoller.Actors.Interface;
using SteamRoller.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamRoller.API.Actors
{
    //Objec tthat stracks state of User library
    public class PlayerActor : Actor, IPlayerActor
    {
        
        private const string StateName = "Player";

        public SteamLibrary UserLibrary { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DemoActor"/> class.
        /// </summary>
        /// <param name="service">Actor Service hosting the actor.</param>
        /// <param name="actorId">Actor Id.</param>
        public PlayerActor(ActorHost service)
            : base(service)
        {
        } 


        public async Task UploadLibrary(SteamLibrary library)
        {
            await this.StateManager.SetStateAsync<SteamLibrary>(StateName, library);
      
        }

        public async Task<List<Game>> ReadyToPlayGames()
        {
            return await FilterGamesBy(StateFlags.Ready);
        }

        public async Task<List<Game>> FilterGamesBy(StateFlags steamState)
        {
            var state =  await this.StateManager.GetStateAsync<SteamLibrary>(StateName);
            return state.FilterBy(steamState);
        }



    }
}
