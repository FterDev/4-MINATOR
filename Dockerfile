#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FourMinator/FourMinator.csproj", "FourMinator/"]
COPY ["FourMinator.Auth/FourMinator.Auth.csproj", "FourMinator.Auth/"]
COPY ["FourMinator.Game/FourMinator.Game.csproj", "FourMinator.Game/"]
COPY ["Fourminator.Persistence/Fourminator.Persistence.csproj", "Fourminator.Persistence/"]
COPY ["FourMinator.Robot/FourMinator.Robot.csproj", "FourMinator.Robot/"]
RUN dotnet restore "./FourMinator/FourMinator.csproj"
COPY . .
WORKDIR "/src/FourMinator"
RUN dotnet build "./FourMinator.csproj"  -o /app/build

FROM build AS publish

RUN dotnet publish "./FourMinator.csproj"  -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FourMinator.dll"]