FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/AmourLink.Matching/AmourLink.Matching.csproj", "Services/AmourLink.Matching/"]
COPY ["Infrastructure/AmourLink.Infrastructure/AmourLink.Infrastructure.csproj", "Infrastructure/AmourLink.Infrastructure/"]
COPY ["Infrastructure/AmourLink.InternalCommunication/AmourLink.InternalCommunication.csproj", "Infrastructure/AmourLink.InternalCommunication/"]
RUN dotnet restore "Services/AmourLink.Matching/AmourLink.Matching.csproj"
COPY . .
WORKDIR "/src/Services/AmourLink.Matching"
RUN dotnet build "AmourLink.Matching.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AmourLink.Matching.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AmourLink.Matching.dll"]
