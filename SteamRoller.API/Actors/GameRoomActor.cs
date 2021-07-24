using Dapr.Actors;
using Dapr.Actors.Runtime;
using Microsoft.Extensions.Logging;
using SteamRoller.API.Extensions;
using SteamRoller.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamRoller.API.Actors
{
    //RoomActor Requires at least 2 players
    //Tracks state of users library
    //has unique RoomCode - way to join the game. 
    public class GameRoomActor : Actor, IGameRoomActor
    {
        private const string StateName = "GameRoom";

        private Guid RoomId;

        public List<Guid> PlayerIds { get; set; }

        public GameRoomActor(ActorHost host, ActorId actorId)
            : base(host)
        {
            RoomId = Guid.Parse(actorId.GetId());
        }


        


        //AddnewUsers to the list
        public void GenerateRoomCode()
        {
            //Distinct RoomCode Service
            //Tracks ID's  -- probaly best done in the APi layer more of an implimentation detail of the endpoints

        }


        public async Task AddPlayer(Guid PlayerId)
        {
            PlayerIds.Add(PlayerId);
            Logger.LogInformation($"Game Room:{RoomId} added Player {PlayerId} ");
        }


        public async Task<Game> Rumble() 
        {
            List<PlayerInformation> playerData = new List<PlayerInformation>();
            foreach (var player in PlayerIds)
            {
                List<Game> playerGames = new List<Game>();
                //GetActiveGames
                playerData.Add(new PlayerInformation { Id = player, Games = playerGames });
            }
            
            var GameList = playerData.IntersectMany(x => x.Games).ToList();

            var random = new Random();
            int index = random.Next(GameList.Count);
            return GameList[index];
        }

       
    }


   

    public record PlayerInformation
    {
        public Guid Id { get; init; }

        public List<Game> Games { get; init; }
    }
}
