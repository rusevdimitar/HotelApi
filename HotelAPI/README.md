# Hotel Web API

This C# .NET Web API provides endpoints to retrieve hotel and room information based on certain queries. It delivers JSON outputs for easy consumption by client applications.

## Endpoints

### 1. Get all rooms for a specific hotel code
GET /api/hotel/rooms/{hotelCode}

Returns a list of all rooms including their prices for the specified hotel code.

### 2. Get the cheapest hotel for a searched room type
GET /api/hotel/cheapesthotel?roomType={roomType}

Returns the name of the cheapest hotel for the specified room type.

### 3. Get all hotels in a city sorted by local category
GET /api/hotel/hotels/{city}

Returns all hotels in the specified city, sorted in descending order by their local category.

## Usage

1. Clone the repository or download the source code from https://github.com/rusevdimitar/TestProjects/tree/master/HotelAPI
2. Open the solution in Visual Studio 2022 17.9.0
3. Build and run the project.
4. Use Swagger UI as the project is started or use any HTTP client (e.g., Postman) to send requests to the provided endpoints.
5. For authorization request use key : "Key", value : "MySecretKeyForAuthenticationOfApplication"
		e.g.
			{
  "browserDisplayMode": 0,
  "browserTimeout": 0,
  "frontChannelExtraParameters": [
    {
      "key": "Key",
      "value": "MySecretKeyForAuthenticationOfApplication"
    }
  ],
  "backChannelExtraParameters": [
    {
      "key": "string",
      "value": "string"
    }
  ]
}

## Dependencies

- .NET 8.0
- .NET Core 3.1 or later
- Microsoft.AspNetCore.Mvc.Core (for Web API)
- Microsoft.Extensions.Caching.Memory (optional, for caching)
- nUnit

## Run in Docker

- Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))
- choco install docker-desktop --version=2.0.0.3
- docker build -t hotel-api .
- docker run -d -p 5000:80 hotel-api
- docker ps






