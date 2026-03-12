# GameStore.Api

A RESTful API for a game store built with .NET 10 and SQLite.

## Prerequisites

- .NET 10.0 SDK
- A code editor (VS Code, Visual Studio, etc.)

## Installation

```bash
dotnet restore
dotnet build
```

## Running the Application

```bash
dotnet run
```

The API will be available at `http://localhost:5171`

## Database

The application uses SQLite. The database file is `GameStore.db`.

Database migrations are applied automatically on startup.

## Folder Structure

```
GameStore.Api/
├── Data/
│   ├── DataExtensions.cs         # Database configuration extension
│   ├── GameStoreContext.cs       # EF Core DbContext
│   └── Migrations/               # EF Core migrations
├── Dtos/
│   ├── CreateGameDto.cs          # DTO for creating games
│   ├── GameDetailsDto.cs         # DTO for game details response
│   ├── GameSummaryDto.cs         # DTO for game list response
│   ├── GenreDto.cs               # DTO for genre response
│   └── UpdateGameDto.cs          # DTO for updating games
├── Endpoints/
│   ├── GameEndpoints.cs          # Game API endpoints
│   └── GenresEndpoints.cs        # Genre API endpoints
├── Models/
│   ├── Game.cs                   # Game entity
│   └── Genre.cs                  # Genre entity
├── Properties/
│   └── launchSettings.json       # Launch configuration
├── appsettings.json              # App settings
├── appsettings.Development.json  # Development settings
├── GameStore.db                  # SQLite database file
├── GameStore.Api.csproj          # Project file
└── Program.cs                    # Application entry point
```

## API Endpoints

### Games

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/games` | Get all games |
| GET | `/games/{id}` | Get a game by ID |
| POST | `/games` | Create a new game |
| PUT | `/games/{id}` | Update a game |
| DELETE | `/games/{id}` | Delete a game |

### Genres

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/genres` | Get all genres |

## Technology Stack

- .NET 10.0
- ASP.NET Core
- Entity Framework Core 10.0.3
- SQLite
- FluentValidation (for request validation)
