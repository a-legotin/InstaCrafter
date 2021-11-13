FROM  mcr.microsoft.com/dotnet/aspnet:5.0-bullseye-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-bullseye-slim AS build
WORKDIR /src
COPY ["src/services/InstaCrafter.ApiGateway/InstaCrafter.ApiGateway.csproj", "services/InstaCrafter.ApiGateway/"]

RUN dotnet restore "services/InstaCrafter.ApiGateway/InstaCrafter.ApiGateway.csproj"
COPY src/. .
WORKDIR "/src/services/InstaCrafter.ApiGateway"
RUN dotnet build "InstaCrafter.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InstaCrafter.ApiGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "InstaCrafter.ApiGateway.dll"]
