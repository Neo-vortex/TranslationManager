# Multi-stage build
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TranslationManager.csproj", "./"]
RUN dotnet restore "TranslationManager.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "TranslationManager.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TranslationManager.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set default environment variables (can be overridden at runtime)
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ConnectionStrings__DefaultConnection=""
ENV ConnectionStrings__Redis=""

ENTRYPOINT ["dotnet", "TranslationManager.dll"]