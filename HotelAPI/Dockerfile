# Use the official .NET Core SDK as a parent image
# TODO: migrate to SDK 8.0
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app

# Copy the project file and restore any dependencies (use .csproj for the project name)
COPY HotelAPI.sln ./
COPY HotelAPI/*.csproj ./HotelAPI/
COPY Contracts/*.csproj ./Contracts/
COPY LoggerService/*.csproj ./LoggerService/

RUN dotnet restore

# Copy the rest of the application code
COPY . .

WORKDIR /app/Contracts
RUN dotnet build -c Release -o /app

WORKDIR /app/LoggerService
RUN dotnet build -c Release -o /app

WORKDIR /app/HotelAPI
RUN dotnet build -c Release -o /app

# Publish the application
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expose the port your application will run on
EXPOSE 5000

# Start the application
ENTRYPOINT ["dotnet", "HotelAPI.dll"]