FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/AmourLink.Recommendation/AmourLink.Recommendation.csproj", "Services/AmourLink.Recommendation/"]
COPY ["Infrastructure/AmourLink.Infrastructure/AmourLink.Infrastructure.csproj", "Infrastructure/AmourLink.Infrastructure/"]
RUN dotnet restore "Services/AmourLink.Recommendation/AmourLink.Recommendation.csproj"
COPY . .
WORKDIR "/src/Services/AmourLink.Recommendation"
RUN dotnet build "AmourLink.Recommendation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AmourLink.Recommendation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AmourLink.Recommendation.dll"]
