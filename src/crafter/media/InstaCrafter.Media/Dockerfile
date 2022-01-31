FROM  mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build

RUN export PATH="$PATH:/root/.dotnet/tools"

ARG NUGET_USER
ARG NUGET_PASS

RUN dotnet nuget add source "https://git.codegarage.ru/api/v4/projects/2/packages/nuget/index.json"  --name cozybus --username $NUGET_USER --password $NUGET_PASS --store-password-in-clear-text  
RUN dotnet nuget add source "https://git.codegarage.ru/api/v4/projects/4/packages/nuget/index.json"  --name instasharper --username $NUGET_USER --password $NUGET_PASS --store-password-in-clear-text  

WORKDIR /src
COPY ["src/crafter/media/InstaCrafter.Media/InstaCrafter.Media.csproj", "crafter/InstaCrafter.Media/"]

RUN dotnet restore "crafter/InstaCrafter.Media/InstaCrafter.Media.csproj"
COPY src/. .
WORKDIR "/src/crafter/InstaCrafter.Media"
RUN dotnet build "InstaCrafter.Media.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InstaCrafter.Media.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InstaCrafter.Media.dll"]
