using Gameloop.Vdf;
using Gameloop.Vdf.Linq;
using SteamRoller.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SteamRoller.Client.Services
{
    public class SteamLibraryService : Steam
    {
        public SteamLibrary Library { get; set; }


        public List<string> Locations = new List<string>();
 


        public SteamLibraryService(ILogger<SteamLibraryService> logger)
        {

            Locations.Add(base.InstallPath);
            Library = new SteamLibrary();
            GetUserLibraries();
            GetInstalledGameFiles();
        }

        private void GetUserLibraries()
        {
            var libraryMetadataFile = $@"{InstallPath}\steamapps\libraryfolders.vdf";
            Console.WriteLine(libraryMetadataFile);
            VProperty folders = VdfConvert.Deserialize(File.ReadAllText(libraryMetadataFile));
            foreach (VProperty item in folders.Value)
            {
                if (Path.IsPathFullyQualified(item.Value.ToString()))
                    Locations.Add(item.Value.ToString());
            }
        }

        private void GetInstalledGameFiles()
        {
            foreach (string location in Locations)
            {
                var files = Directory.GetFiles($"{location}\\steamapps", "*.acf");
                Library.GameMetadata.AddRange(files.ToList());
            }

            ParseAcfFiles();
        }

        private void ParseAcfFiles()
        {
            foreach (var metadatafile in Library.GameMetadata)
            {
                try
                {
                    dynamic volvo = VdfConvert.Deserialize(File.ReadAllText(metadatafile));
                    Game game = new Game { AppId = volvo.Value.appid.ToString(), Name = volvo.Value.name.ToString(), StateFlags = volvo.Value.StateFlags.ToString(), Platform = Platform.Steam };
                    Library.Games.Add(game);
                }
                catch (Exception ex)
                {

                    
                }
                 
            }
        }

    }
}
