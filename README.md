# Music Label API

## Overview
A RESTful API built with ASP.NET Core and EF Core to manage music labels, albums, and artists. The API provides CRUD operations and structured data management.

## Technologies Used
- ASP.NET Core
- Entity Framework Core (EF Core)
- SQL Server
- AutoMapper
- Swagger (OpenAPI)

## Database Structure
- **Music Labels**: Stores label details.
- **Albums**: Belongs to a music label and has multiple artists.
- **Artists**: Can contribute to multiple albums.

## Setup Instructions
1. Clone the repository.
2. Install dependencies using `.NET CLI`.
3. Configure the database connection in `appsettings.json`.
4. Run database migrations:
   ```sh
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
5. Start the API:
   ```sh
   dotnet run
   ```

## Documentation
Swagger UI is available at:
```
http://localhost:<port>/swagger
```
