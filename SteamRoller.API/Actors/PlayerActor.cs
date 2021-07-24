using SteamRoller.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamRoller.API.Actors
{
    //Objec tthat stracks state of User library
    public class PlayerActor
    {
        //ID random GUID
        public int Id { get; set; }

        public SteamLibrary UserLibrary { get; set; }

        public void ReceiveLibrary(SteamLibrary library)
        {
            UserLibrary = library;
        }

        public List<Game> ReadyToPlayGames()
        {
            return UserLibrary.FilterBy(StateFlags.Ready);
        }



    }
}
