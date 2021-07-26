using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace SteamRoller.Client.Mappings
{

    internal class SteamLibraryProfile : Profile
    {
        public SteamLibraryProfile()
        {
            CreateMap<SteamRoller.Core.SteamLibrary, SteamRoller.API.Client.SteamLibrary>();
            CreateMap<SteamRoller.Core.Game, SteamRoller.API.Client.Game>();
        }
    }


}