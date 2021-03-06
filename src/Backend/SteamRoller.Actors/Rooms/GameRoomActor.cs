using Dapr.Actors;
using Dapr.Actors.Client;
using Dapr.Actors.Runtime;
using Microsoft.Extensions.Logging;
using SteamRoller.Actors.Extensions;
using SteamRoller.Actors.Interface;
using SteamRoller.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamRoller.Actors.Rooms
{
    //RoomActor Requires at least 2 players
    //Tracks state of users library
    //has unique RoomCode - way to join the game. 
    [Actor(TypeName = "GameRoomActor")]
    public class GameRoomActor : Actor, IGameRoomActor
    {
        private const string StateName = "GameRoom";

        public List<Guid> PlayerIds { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DemoActor"/> class.
        /// </summary>
        /// <param name="service">Actor Service hosting the actor.</param>
        /// <param name="actorId">Actor Id.</param>
        public GameRoomActor(ActorHost service)
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
            Console.WriteLine($"Activating actor id: {this.Id}");
            var availablestate = Task.Run(() =>  StateManager.TryGetStateAsync<List<Guid>>(StateName ) ).Result;
            Logger.LogInformation($"Tried to Load RoomStateState: {this.Id}");
            PlayerIds = availablestate.HasValue ? availablestate.Value : new();
            return Task.CompletedTask;

        }

        //AddnewUsers to the list
        public void GenerateRoomCode()
        {
            //Distinct RoomCode Service
            //Tracks ID's  -- probaly best done in the APi layer more of an implimentation detail of the endpoints

        }

        internal async Task  SaveState(){
            await this.StateManager.SetStateAsync<List<Guid>>(StateName, PlayerIds);
        }


        public async Task AddPlayer(Guid PlayerId)
        {
            if(PlayerIds.Contains(PlayerId)){
                Logger.LogInformation($"Game Room:{this.Id} Player {PlayerId}  already exists");
            }
            else{
                PlayerIds.Add(PlayerId);
                Logger.LogInformation($"Game Room:{this.Id} added Player {PlayerId} ");
                await SaveState();
            }
        }


        public async Task<Game> Rumble()
        {
            List<PlayerInformation> playerData = new List<PlayerInformation>();
            foreach (var player in PlayerIds)
            {
                ActorId actorId = new(player.ToString());
                var proxy = ActorProxy.Create<IPlayerActor>(actorId, "PlayerActor");
                List<Game> playerGames = await proxy.ReadyToPlayGames();
                //GetActiveGames
                playerData.Add(new PlayerInformation { Id = player, Games = playerGames });
            }

            var GameList = playerData.IntersectMany(x => x.Games).ToList();

            var random = new Random();
            int index = random.Next(GameList.Count);
            return GameList[index];
        }

        public Task<List<Guid>> GetPlayerList()
        {
            return Task.FromResult(PlayerIds);
        }


    }




    public record PlayerInformation
    {
        public Guid Id { get; init; }

        public List<Game> Games { get; init; }
    }
}
