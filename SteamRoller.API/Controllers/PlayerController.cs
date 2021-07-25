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
    public class PlayerController : ControllerBase
    { 
        private readonly ILogger<PlayerController> _logger;

        public PlayerController(ILogger<PlayerController> logger)
        {
            _logger = logger;
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public  IActionResult CreatePlayer()
        {
            ActorId actorId = ActorId.CreateRandom();
            var proxy =  ActorProxy.Create<IPlayerActor>(actorId, "PlayerActor");

            return Ok(actorId.GetId());            
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<IActionResult> UploadLibrary([FromBody]SteamLibrary library,string userId )
        {
            ActorId actorId = new(userId);
            var proxy =  ActorProxy.Create<IPlayerActor>(actorId, "PlayerActor");
            await proxy.UploadLibrary(library);
            return Ok(actorId.GetId());            
        }
    }
}
