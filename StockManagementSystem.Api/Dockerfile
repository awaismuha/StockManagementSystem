FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["StockManagementSystem.Api.csproj", "./"]
RUN dotnet restore "StockManagementSystem.Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "StockManagementSystem.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StockManagementSystem.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StockManagementSystem.Api.dll"] 