using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SteamRoller.Core;
using SteamRoller.Client.Services;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Http;
using AutoMapper;

namespace SteamRoller.Client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IConfiguration _configuration;

        private IMapper _mapper;
        public string PlayerId { get; set; }

        public Game SelectedGame { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration, IMapper mapper)
        {
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
        }

        public void OnGet()
        {
            SteamLibraryService steam = new SteamLibraryService();

            Console.WriteLine("The app has loaded, no flaky code yet.");
            Console.WriteLine("Initialising");
            //Load up Libraries
            // and get installed games. 
            Console.WriteLine($" you have {steam.Locations.Count} steam libs");
            Console.WriteLine("Welcome to the Steam Pick yer game - innit");
            Console.WriteLine($" you have {steam.Library.Games.Count} games installed");
            Console.WriteLine($" you have {steam.Library.Games.Where(x => x.StateFlags == "4").Count()} state 4 (Ready to play)");
            Console.WriteLine($" you have {steam.Library.Games.Where(x => x.StateFlags == "6").Count()} state 6 (Update scheduled)");

            var random = new Random();
            int index = random.Next(steam.Library.Games.Count);
            SelectedGame = steam.Library.Games[index];
            Console.WriteLine(SelectedGame.Name);
        }


        public async Task OnGetCreatePlayer()
        {

            var sessionPlayerId = HttpContext.Session.GetString("PlayerId");
            if (string.IsNullOrEmpty(sessionPlayerId))
            {
                var uri = _configuration["SteamRoller:Uri"];
                _logger.LogDebug("Reteived URI Config");

                HttpClient httpclient = new();
                httpclient.BaseAddress = new Uri(uri);

                var client = new SteamRoller.API.Client.PlayerClient(uri.ToString(), httpclient);

                _logger.LogDebug("Created Api Client");


                string playerId = await client.Player_CreatePlayerAsync();

                _logger.LogInformation($"Called Api played id: {playerId}");

                HttpContext.Session.SetString("PlayerId", playerId);
            }

        } 

        public async Task OnGetUploadLibrary()
        {

            var sessionPlayerId = HttpContext.Session.GetString("PlayerId");
            if (!string.IsNullOrEmpty(sessionPlayerId))
            {
                SteamLibraryService steam = new SteamLibraryService();


                var uri = _configuration["SteamRoller:Uri"];
                _logger.LogDebug("Reteived URI Config");

                HttpClient httpclient = new();
                httpclient.BaseAddress = new Uri(uri);

                var client = new SteamRoller.API.Client.PlayerClient(uri.ToString(), httpclient);

                _logger.LogDebug("Created Api Client");

                var dest = _mapper.Map<SteamRoller.Core.SteamLibrary, API.Client.SteamLibrary>(steam.Library);

                string uploadresult = await client.Player_UploadLibraryAsync(sessionPlayerId, dest);

                _logger.LogInformation($"Library Uploaded for player {sessionPlayerId}");

            };

        }

    }

}

