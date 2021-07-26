namespace SteamRoller.Core
{
    public record Game
    {
        public string AppId { get; set; }

        public string Name { get; set; }

        public string StateFlags { get; set; }

        public Platform Platform { get; set; }


    }



}


