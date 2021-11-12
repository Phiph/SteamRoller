#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Backend/SteamRoller.API/SteamRoller.API.csproj", "Backend/SteamRoller.API/"]
COPY ["SteamRoller.Core/SteamRoller.Core.csproj", "SteamRoller.Core/"]
COPY ["Backend/SteamRoller.Actor.Interface/SteamRoller.Actor.Interface.csproj", "Backend/SteamRoller.Actor.Interface/"]
RUN dotnet restore "Backend/SteamRoller.API/SteamRoller.API.csproj"
COPY . .
WORKDIR "/src/Backend/SteamRoller.API"
RUN dotnet build "SteamRoller.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SteamRoller.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SteamRoller.API.dll"]