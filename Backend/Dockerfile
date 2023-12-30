# Use the official .NET 8.0 SDK image as a build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /kds-backend

# Copy the .csproj and restore as distinct layers
COPY *.csproj .
RUN dotnet restore

# Copy the remaining source code and build the application
COPY . .
RUN dotnet publish -c Release -o out

# Use a Windows base image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /kds-backend
COPY --from=build /kds-backend/out .

# Copy the SSL folder from the host to the container
COPY ./ssl /kds-backend/ssl
# Create a volume for the SQLite database file
# VOLUME /Database

# Start the application
ENTRYPOINT ["dotnet", "kds.Backend.dll"]