#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Backend/SteamRoller.Actors/SteamRoller.Actors.csproj", "Backend/SteamRoller.Actors/"]
COPY ["SteamRoller.Core/SteamRoller.Core.csproj", "SteamRoller.Core/"]
COPY ["Backend/SteamRoller.Actor.Interface/SteamRoller.Actor.Interface.csproj", "Backend/SteamRoller.Actor.Interface/"]
RUN dotnet restore "Backend/SteamRoller.Actors/SteamRoller.Actors.csproj"
COPY . .
WORKDIR "/src/Backend/SteamRoller.Actors"
RUN dotnet build "SteamRoller.Actors.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SteamRoller.Actors.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SteamRoller.Actors.dll"]