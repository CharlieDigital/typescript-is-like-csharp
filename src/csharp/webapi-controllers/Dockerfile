# Build layer
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Our project layer so we only update on new deps
COPY ./webapi-controllers.csproj ./webapi-controllers.csproj
RUN dotnet restore

# Copy over code and publish
COPY ./Program.cs ./Program.cs
RUN dotnet publish ./webapi-controllers.csproj -o /app/published-app --configuration Release

# Runtime layer
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/published-app /app

ENTRYPOINT [ "dotnet", "/app/webapi-controllers.dll" ]
