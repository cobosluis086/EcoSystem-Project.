# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore EcoSystem.API/EcoSystem.API.csproj

RUN dotnet publish EcoSystem.API/EcoSystem.API.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://0.0.0.0:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "EcoSystem.API.dll"]