using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SteamRoller.Client.Services;
using SteamRoller.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamRoller.Client.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public Game SelectedGame;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
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
    }
}
