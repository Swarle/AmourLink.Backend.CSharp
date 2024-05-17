FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AmourLink.Gateway/AmourLink.Gateway.csproj", "AmourLink.Gateway/"]
RUN dotnet restore "AmourLink.Gateway/AmourLink.Gateway.csproj"
COPY . .
WORKDIR "/src/AmourLink.Gateway"
RUN dotnet build "AmourLink.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AmourLink.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AmourLink.Gateway.dll"]
