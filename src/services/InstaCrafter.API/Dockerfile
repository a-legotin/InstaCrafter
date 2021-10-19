FROM mcr.microsoft.com/dotnet/aspnet:5.0-bullseye-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-bullseye-slim AS build
WORKDIR /src
COPY ["services/InstaCrafter.API/InstaCrafter.API.csproj", "InstaCrafter.API/"]
RUN dotnet restore "services/InstaCrafter.API/InstaCrafter.API.csproj"
COPY . .
WORKDIR "/src/InstaCrafter.API"
RUN dotnet build "InstaCrafter.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InstaCrafter.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InstaCrafter.API.dll"]
