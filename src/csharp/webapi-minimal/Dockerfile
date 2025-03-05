# Build layer
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Our project layer so we only update on new deps
COPY ./webapi-minimal.csproj ./webapi-minimal.csproj
RUN dotnet restore

# Copy over code and publish
COPY ./Program.cs ./Program.cs
RUN dotnet publish ./webapi-minimal.csproj -o /app/published-app --configuration Release

# Runtime layer
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/published-app /app

ENTRYPOINT [ "dotnet", "/app/webapi-minimal.dll" ]
