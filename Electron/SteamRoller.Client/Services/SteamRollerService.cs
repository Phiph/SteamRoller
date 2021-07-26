using System.Xml;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SteamRoller.API.Client;

namespace SteamRoller.Client.Services
{

    public class SteamRollerService : ISteamRollerService
    {
        private readonly IMapper _mapper;
        ILogger<SteamRollerService> _logger;

        private IPlayerClient _playerClient;
        private IRoomClient _roomClient;
        SteamLibraryService steam;
        public SteamRollerService(ILogger<SteamRollerService> logger, IMapper mapper, IPlayerClient playerClient, IRoomClient RoomClient)
        {
            _logger = logger;
            _mapper = mapper;
            _playerClient = playerClient;
            _roomClient = RoomClient;
            steam = new SteamLibraryService();
        }

        public async Task<string> CreatePlayerId()
        {
            _logger.LogInformation($"Getting User id");
            return await _playerClient.CreatePlayerAsync();
        }

        public async Task<string> NewRoom(string userId)
        {
            var roomid = await _roomClient.CreateAsync();

            //Join room
            await JoinRoom(roomid, userId);

            //Todo:Claim Room as Host

            return roomid;
        }

        public async Task<string> JoinRoom(string roomid, string userId)
        {
            return await _roomClient.JoinRoomAsync(roomid, userId);
        }


        public async Task UploadLibrary(string userId)
        {
            var dest = _mapper.Map<SteamRoller.Core.SteamLibrary, API.Client.SteamLibrary>(steam.Library);
            string uploadresult = await _playerClient.UploadLibraryAsync(userId, dest);
            _logger.LogInformation($"Library Uploaded for player {userId}");
        }

        public async Task<Game> Rumble(string roomId)
        {
            return await _roomClient.PickGameAsync(roomId);
        }
    }


    public interface ISteamRollerService
    {
        Task<string> CreatePlayerId(); 
        Task UploadLibrary(string userId);
        Task<string> NewRoom(string userId);
        Task<string> JoinRoom(string roomid, string userId);

        Task<Game> Rumble(string roomId);
    }


}