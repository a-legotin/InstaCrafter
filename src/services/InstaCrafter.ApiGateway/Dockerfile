FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["services/InstaCrafter.ApiGateway/InstaCrafter.ApiGateway.csproj", "InstaCrafter.ApiGateway/"]
RUN dotnet restore "services/InstaCrafter.ApiGateway/InstaCrafter.ApiGateway.csproj"
COPY . .
WORKDIR "/src/InstaCrafter.ApiGateway"
RUN dotnet build "InstaCrafter.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InstaCrafter.ApiGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InstaCrafter.ApiGateway.dll"]
