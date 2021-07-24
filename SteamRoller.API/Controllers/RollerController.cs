using Dapr.Actors;
using Dapr.Actors.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SteamRoller.API.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamRoller.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RollerController : ControllerBase
    {
       

        private readonly ILogger<RollerController> _logger;

        public RollerController(ILogger<RollerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> CreateRoom()
        {
            var playerId = Guid.NewGuid();
            ActorId actorId = new ActorId(Guid.NewGuid().ToString());

            IGameRoomActor serviceDutyResultsActor = ActorProxy.Create<IGameRoomActor>(actorId, "GameRoomActor");
            await serviceDutyResultsActor.AddPlayer(playerId);

            return Ok(actorId);
        }

        [HttpPost]
        public async Task<IActionResult> JoinRoom(string gameRoomId)
        {
            var playerId = Guid.NewGuid();
            ActorId actorId = new ActorId(gameRoomId);
            IGameRoomActor serviceDutyResultsActor = ActorProxy.Create<IGameRoomActor>(actorId, "GameRoomActor");
           
            
            await serviceDutyResultsActor.AddPlayer(playerId);


            return Ok();
        }


    }
}
