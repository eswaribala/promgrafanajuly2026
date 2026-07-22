# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

EXPOSE 7078

ENV ASPNETCORE_URLS=http://+:7078

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["InventoryAPI/InventoryAPI.csproj", "InventoryAPI/"]
RUN dotnet restore "InventoryAPI/InventoryAPI.csproj"

COPY . .

WORKDIR "/src/InventoryAPI"

RUN dotnet build "InventoryAPI.csproj" \
    -c Release \
    -o /app/build \
    --no-restore

# Publish image
FROM build AS publish

RUN dotnet publish "InventoryAPI.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false \
    --no-restore

# Final runtime image
FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "InventoryAPI.dll"]