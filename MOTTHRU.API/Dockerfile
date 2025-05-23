FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MOTTHRU.API/MOTTHRU.API.csproj", "MOTTHRU.API/"]
RUN dotnet restore "MOTTHRU.API/MOTTHRU.API.csproj"
COPY . .
WORKDIR "/src/MOTTHRU.API"
RUN dotnet build "MOTTHRU.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MOTTHRU.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MOTTHRU.API.dll"]
