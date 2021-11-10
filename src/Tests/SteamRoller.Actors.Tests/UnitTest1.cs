using Microsoft.VisualStudio.TestTools.UnitTesting;
using SteamRoller.Actors.Extensions;
using SteamRoller.Actors.Rooms;
using SteamRoller.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SteamRoller.Actors.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SinglePlayerGame_Test()
        {
            List<PlayerInformation> playerData = new List<PlayerInformation>();

            PlayerInformation player1 = new PlayerInformation
            {
                Id = Guid.Parse("70447c36-4e87-40f9-8efe-78a484c18bc0"),
                Games = new List<Game>
                {
                     new Game{ AppId = "1016920", Name = "Unrailed!", StateFlags = "4", Platform = 0 }
                   
                }

            };

            playerData.Add(player1);

            var GameList = playerData.IntersectMany(x => x.Games).ToList();
            Console.WriteLine($"selectedgame {GameList.First().Name}");

            Assert.IsTrue(GameList.Count() == 1, "Single Player Game list doesn't work");
      
        }


        [TestMethod]
        public void TwoPlayerGameWithSamePlayerIds_Test()
        {
            List<PlayerInformation> playerData = new List<PlayerInformation>();

            PlayerInformation player1 = new PlayerInformation
            {
                Id = Guid.Parse("70447c36-4e87-40f9-8efe-78a484c18bc0"),
                Games = new List<Game>
                {
                     new Game{ AppId = "1016920", Name = "Unrailed!", StateFlags = "4", Platform = 0 }

                }

            };

            PlayerInformation player2 = new PlayerInformation
            {
                Id = Guid.Parse("70447c36-4e87-40f9-8efe-78a484c18bc0"),
                Games = new List<Game>
                {
                     new Game{ AppId = "1016920", Name = "Unrailed!", StateFlags = "4", Platform = 0 }

                }

            };


            playerData.Add(player1);
            playerData.Add(player2);
            var GameList = playerData.IntersectMany(x => x.Games).ToList();


            Assert.IsTrue(GameList.Count() == 1, "Single Player Game list doesn't work");

        }

        [TestMethod]
        public void TwoPlayerGameWithDifferentPlayerIds_Test()
        {
            List<PlayerInformation> playerData = new List<PlayerInformation>();

            PlayerInformation player1 = new PlayerInformation
            {
                Id = Guid.Parse("7641a5d6-3539-4379-9e84-3f96316b81fe"),
                Games = new List<Game>
                {
                     new Game{ AppId = "1016920", Name = "Unrailed!", StateFlags = "4", Platform = 0 },
                     new Game{ AppId = "1557740", Name = "ROUNDS", StateFlags = "4", Platform = 0 }

                }

            };

            PlayerInformation player2 = new PlayerInformation
            {
                Id = Guid.Parse("70447c36-4e87-40f9-8efe-78a484c18bc0"),
                Games = new List<Game>
                {
                     new Game{ AppId = "1016920", Name = "Unrailed!", StateFlags = "4", Platform = 0 },
                     new Game{ AppId = "1352340", Name = "Rolling Hamster", StateFlags = "4", Platform = 0 },
                     new Game{ AppId = "1557740", Name = "ROUNDS", StateFlags = "4", Platform = 0 }
                }

            };


            playerData.Add(player1);
            playerData.Add(player2);
            var GameList = playerData.IntersectMany(x => x.Games).ToList();
            Console.WriteLine($"There are  {GameList.Count} matching games");

        
            var random = new Random();
            int index = random.Next(GameList.Count);
           

            Console.WriteLine($"selectedgame {GameList[index].Name}");


            Assert.IsTrue(GameList.Count() >= 1, "Single Player Game list doesn't work");

        }
    }
}
