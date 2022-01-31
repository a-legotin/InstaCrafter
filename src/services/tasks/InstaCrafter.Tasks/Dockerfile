FROM  mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /src

ARG NUGET_USER
ARG NUGET_PASS

RUN dotnet nuget add source "https://git.codegarage.ru/api/v4/projects/2/packages/nuget/index.json"  --name cozybus --username $NUGET_USER --password $NUGET_PASS --store-password-in-clear-text  
RUN dotnet nuget add source "https://git.codegarage.ru/api/v4/projects/4/packages/nuget/index.json"  --name instasharper --username $NUGET_USER --password $NUGET_PASS --store-password-in-clear-text  

COPY ["src/services/tasks/InstaCrafter.Tasks/InstaCrafter.Tasks.csproj", "services/tasks/InstaCrafter.Tasks/"]

RUN dotnet restore "services/tasks/InstaCrafter.Tasks/InstaCrafter.Tasks.csproj"
COPY src/. .
WORKDIR "/src/services/tasks/InstaCrafter.Tasks"
RUN dotnet build "InstaCrafter.Tasks.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InstaCrafter.Tasks.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8082
ENTRYPOINT ["dotnet", "InstaCrafter.Tasks.dll"]
