FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-bullseye-slim AS build
WORKDIR /src
COPY ["crafter/InstaCrafter.Media/InstaCrafter.Media.csproj", "InstaCrafter.Media/"]
RUN dotnet restore "crafter/InstaCrafter.Media/InstaCrafter.Media.csproj"
COPY . .
WORKDIR "/src/InstaCrafter.Media"
RUN dotnet build "InstaCrafter.Media.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InstaCrafter.Media.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InstaCrafter.Media.dll"]
