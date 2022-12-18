FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /Backend/Backend

# Copy everything
COPY /Backend/Backend/. ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /Backend/Backend
COPY --from=build-env /Backend/Backend/out .
ENTRYPOINT ["dotnet", "Backend.dll"]