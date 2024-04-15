FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FourMinator/FourMinator.csproj", "FourMinator/"]
RUN dotnet restore "FourMinator/FourMinator.csproj"
COPY . .
WORKDIR "/src/FourMinator"
RUN dotnet build "FourMinator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FourMinator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FourMinator.dll"]
