FROM  mcr.microsoft.com/dotnet/aspnet:5.0-bullseye-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-bullseye-slim AS build
WORKDIR /src
COPY ["src/services/InstaCrafter.Identity/InstaCrafter.Identity.csproj", "services/InstaCrafter.Identity/"]

RUN dotnet restore "services/InstaCrafter.Identity/InstaCrafter.Identity.csproj"
COPY src/. .
WORKDIR "/src/services/InstaCrafter.Identity"
RUN dotnet build "InstaCrafter.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InstaCrafter.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "InstaCrafter.Identity.dll"]
