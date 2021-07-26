using Gameloop.Vdf;
using Gameloop.Vdf.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SteamRoller.Core
{
    public class SteamLibrary
    {


        public SteamLibrary()
        {

        }

        public List<string> Locations = new List<string>();
        public List<string> GameMetadata = new List<string>();
        public List<Game> Games = new List<Game>();


        public List<Game> FilterBy(StateFlags GameState)
        {
            return Games.Where(x => x.StateFlags == ((int)GameState).ToString()).ToList();
        }

    }



    public enum StateFlags
    {
        Ready = 4,
        UpdateRequired = 6
    }



}


