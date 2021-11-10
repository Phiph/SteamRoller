using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SteamRoller.Client.Services;
using SteamRoller.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SteamRoller.Client.Pages
{
    public class PlayModel : PageModel
    {
        private readonly ILogger<PlayModel> _logger;
        private readonly IConfiguration _configuration;

        private ISteamRollerService _rollerService;
        private IMapper _mapper;
        public Game SelectedGame;

        public PlayModel(ILogger<PlayModel> logger, IConfiguration configuration, IMapper mapper, ISteamRollerService roller)
        {
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
            _rollerService = roller;

        }


        public async Task OnPost()
        {
            var sessionPlayerId = HttpContext.Session.GetString("PlayerId");
            await _rollerService.JoinRoom(Request.Form["roomId"], sessionPlayerId);

        }
        public async Task<IActionResult> OnPostRegisterAsync()
        {
            var sessionPlayerId = HttpContext.Session.GetString("PlayerId");
            await _rollerService.JoinRoom(Request.Form["roomId"], sessionPlayerId);
            return RedirectToPage();
        } 
        public async Task<IActionResult> OnPostCreateRoom()
        {
            //PlaySinglePlayer();
            var sessionPlayerId = HttpContext.Session.GetString("PlayerId");
            if (!string.IsNullOrEmpty(sessionPlayerId))
            {
                var roomId = await _rollerService.NewRoom(sessionPlayerId);

                HttpContext.Session.SetString("ActiveRoomId", roomId);
            }
            else
            {
                await SetUserIdAndUploadLibrary();
            }
            return RedirectToPage();
        }
        public void OnPostJoinRoom()
        {

        }
        private async Task SetUserIdAndUploadLibrary()
        {
            string playerId = await _rollerService.CreatePlayerId();
            await _rollerService.UploadLibrary(playerId);

            HttpContext.Session.SetString("PlayerId", playerId);
            _logger.LogInformation($"PlayerId Set: {playerId}");
        }


        public async Task OnGetPlayerId()
        {
            await SetUserIdAndUploadLibrary();
        }


        public async Task OnGetRumble()
        {
            var roomId = HttpContext.Session.GetString("ActiveRoomId");
            if (!string.IsNullOrEmpty(roomId))
            {

                var game = await _rollerService.Rumble(roomId);

                //Todo Add AutoMapper
                SelectedGame = new()
                {
                    AppId = game.AppId,
                    Name = game.Name,
                    //Todo:Change this
                    Platform = Platform.Steam,
                    StateFlags = game.StateFlags
                };
            }
        }
    }
}
