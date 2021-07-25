using Dapr.Actors;
using Dapr.Actors.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SteamRoller.Actors.Interface;
using SteamRoller.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamRoller.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RoomController : ControllerBase
    {
       

        private readonly ILogger<RoomController> _logger;

        public RoomController(ILogger<RoomController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        
     
        public string Create()
        {
            ActorId actorId = ActorId.CreateRandom();
            var proxy =  ActorProxy.Create<IGameRoomActor>(actorId, "GameRoomActor");
            //var serviceDutyResultsActor = ActorProxy.Create<IGameRoomActor>(actorId, "GameRoomActor");
            return actorId.GetId();
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> JoinRoom(string gameRoomId, string playerId)
        {
            ActorId actorId = new(gameRoomId);
            IGameRoomActor serviceDutyResultsActor = ActorProxy.Create<IGameRoomActor>(actorId, "GameRoomActor");

            await serviceDutyResultsActor.AddPlayer(Guid.Parse(playerId));

            return Ok();
        }


        [HttpGet]
        public async Task<List<Guid>> GetPlayers(string gameRoomId)
        {
            var playerId = Guid.NewGuid();
            ActorId actorId = new ActorId(gameRoomId);
            IGameRoomActor serviceDutyResultsActor = ActorProxy.Create<IGameRoomActor>(actorId, "GameRoomActor");
            return await serviceDutyResultsActor.GetPlayerList();
            
        }


        public async Task<Game> PickGame(string gameRoomId)
        {
            ActorId actorId = new ActorId(gameRoomId);
            IGameRoomActor serviceDutyResultsActor = ActorProxy.Create<IGameRoomActor>(actorId, "GameRoomActor");
            var res = await serviceDutyResultsActor.Rumble();
            return res;
        }


    }
}
