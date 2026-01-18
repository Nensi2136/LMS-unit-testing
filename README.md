# LMS (Learning Management System) - ASP.NET Core Web API

## Overview
This repository contains a simple **ASP.NET Core Web API** project for an LMS (Learning Management System).

### Key tech
- **.NET**: `net8.0`
- **API**: ASP.NET Core Web API
- **Database**: SQL Server via **Entity Framework Core**
- **API Docs**: Swagger / OpenAPI

## Solution Structure
- `LMS.sln` - Visual Studio solution
- `LMS.csproj` - Main Web API project
- `Controllers/` - API controllers
  - `UserController` - CRUD endpoints for users
  - `MaterialController` - CRUD endpoints for materials
  - `SubjectController` - Sample subject endpoint (placeholder)
  - `SubjectWiseFacultyController` - Placeholder controller
- `Data/AppDbContext.cs` - EF Core DbContext
- `Models/` - Entity models
- `Migrations/` - EF Core migrations
- `appsettings.json` - Connection string and logging
- `LMS.http` - Example HTTP requests (for VS/VS Code REST client)

## Prerequisites
- **.NET SDK 8**
- **SQL Server** (LocalDB or SQLExpress recommended)

## Configuration
### Connection string
The API uses the connection string named `DefaultConnection`:

`appsettings.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=LMS;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}
```

Update it to match your SQL Server instance if needed.

## Run the API
### From Visual Studio
- Set the `LMS` project as startup
- Press `F5` or `Ctrl+F5`

Swagger should open automatically in Development.

### From CLI
From the repository root:
```powershell
dotnet restore
dotnet run --project .\LMS.csproj
```

### Default URLs (Development)
From `Properties/launchSettings.json`:
- HTTP: `http://localhost:5073`
- HTTPS: `https://localhost:7264`
- Swagger: `http://localhost:5073/swagger`

## API Endpoints (high level)
### Users
Base route: `/api/User`
- `GET /api/User` - list users
- `GET /api/User/{id}` - get user by id
- `POST /api/User` - create user
- `PUT /api/User/{id}` - update user
- `DELETE /api/User/{id}` - delete user

### Materials
Base route: `/api/Material/{action}`
- `GET /api/Material/GetMaterials`
- `GET /api/Material/GetMaterial?id={id}`
- `POST /api/Material/PostMaterial`
- `PUT /api/Material/PutMaterial?id={id}`
- `DELETE /api/Material/DeleteMaterial?id={id}`

## Database & Migrations (EF Core)
This repo includes an initial migration under `Migrations/`.

### Apply migrations
If you have the EF Core tooling available:
```powershell
# Installs the EF tool if not already installed
# dotnet tool install --global dotnet-ef

# Update database using the migrations
 dotnet ef database update
```

If `dotnet ef` is not found, install it:
```powershell
dotnet tool install --global dotnet-ef
```

## Testing
If you add a test project (xUnit/Moq), you can run tests with:
```powershell
dotnet test
```

If the build fails due to locked files (for example, `LMS.dll` is in use), stop the running API process and run tests again.

## Common Issues
### SQL Server connection errors
- Ensure SQL Server / SQLExpress is running
- Confirm the connection string points to the correct instance
- Verify database permissions for your Windows account

### Port already in use
- Stop the process using the port or change `applicationUrl` in `Properties/launchSettings.json`

## Notes
- Some controllers contain placeholder logic (for example `SubjectController`).

