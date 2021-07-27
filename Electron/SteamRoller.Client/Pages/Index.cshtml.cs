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

            var readytoPlay = steam.Library.FilterBy(StateFlags.Ready);

            var random = new Random();
            int index = random.Next(readytoPlay.Count);
            SelectedGame = readytoPlay[index];
            Console.WriteLine(SelectedGame.Name);
        }


        public async Task OnGetCreatePlayer()
        {

          

        } 

        public async Task OnGetUploadLibrary()
        {

           

        }

    }

}

