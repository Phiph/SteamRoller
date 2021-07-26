using Dapr.Actors.Runtime;
using Microsoft.Extensions.Logging;
using SteamRoller.Actors.Interface;
using SteamRoller.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamRoller.API.Actors
{
    //Objec tthat stracks state of User library
      [Actor(TypeName = "PlayerActor")]
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

        /// <summary>
        /// This method is called whenever an actor is activated.
        /// An actor is activated the first time any of its methods are invoked.
        /// </summary>
        protected override Task OnActivateAsync()
        {
            // Provides opportunity to perform some optional setup.
              Logger.LogInformation($"Activating actor id: {this.Id}");

            var availablestate = Task.Run(() =>  StateManager.TryGetStateAsync<SteamLibrary>(StateName ) ).Result;
               Logger.LogInformation($"Tried to Load LibraryState: {this.Id}");
            UserLibrary = availablestate.HasValue ? availablestate.Value : new();
           
            return Task.CompletedTask;
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
            return UserLibrary.FilterBy(steamState);
        }



    }
}
