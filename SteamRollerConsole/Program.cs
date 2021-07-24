using Gameloop.Vdf.JsonConverter;
using Microsoft.Extensions.DependencyInjection;
using SteamRoller.Client.Services;
using SteamRoller.Core;
using System;
using System.Linq;

namespace SteamRollerConsole
{
    class Program
    {
        static void init()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddScoped<SteamLibrary, ISteamLibraryService>();
        }

        static void Main(string[] args)
        {
            init()



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
            Console.WriteLine(steam.Library.Games[index].Name);

            Console.ReadLine();

        }
    }



}


