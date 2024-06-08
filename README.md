# Shift Logger Application

## Overview

The Shift Logger application is a comprehensive tool designed to help manage and log work shifts efficiently. The system includes an API built with ASP.NET Core and a user interface using Spectre.Console. The application allows users to create, view, and delete work shift records, offering both backend API functionalities and a simple console-based UI for interaction.

## Features

### API

- **CRUD Operations**: Provides endpoints for creating, reading, updating, and deleting shift records.
- **Entity Framework Core**: Utilizes EF Core for database interactions with SQL Server.
- **Swagger Integration**: Includes Swagger for easy API documentation and testing in development mode.

### User Interface

- **Console-Based UI**: Implements a user-friendly console interface using Spectre.Console for interactive operations.
- **Shift Management**: Allows users to create new shifts, view existing shifts in a tabular format, and handle shift data efficiently.
- **HTTP Client Integration**: Interacts with the API via http requests to perform necessary operations.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- Visual Studio or any preferred code editor

### Setup

1. **Clone the Repository**:
    ```bash
    git clone <repository-url>
    cd shift_logger
    ```

2. **Configure Database**:
    Ensure your SQL Server is running and update the connection string in `Program.cs` if necessary:
    ```csharp
    builder.Services.AddDbContext<ShiftContext>(
        optionsAction: opt => opt.UseSqlServer("Server=localhost;Initial Catalog=Shifts;User Id=sa;password=Password123;TrustServerCertificate=true"));
    ```

3. **Run Migrations**:
    Apply migrations to set up the database schema:
    ```bash
    cd API
    dotnet ef database update
    ```

4. **Build and Run the API**:
    ```bash
    dotnet run --project API
    ```

5. **Run the Console UI**:
    Open a new terminal and run:
    ```bash
    dotnet run --project UI
    ```

### API Endpoints

- **GET /api/shifts**: Retrieve all shift records.
- **GET /api/shifts/{id}**: Retrieve a specific shift by ID.
- **POST /api/shifts**: Create a new shift record.
- **DELETE /api/shifts/{id}**: Delete a shift record by ID.

### UI Operations

- **Create a Shift**:
    - Choose the 'c' option.
    - Enter start and end times for the shift.
- **View Shifts**:
    - Choose the 'v' option.
    - Displays a table of all shift records.
- **Exit**:
    - Choose the 'e' option to exit the application.
