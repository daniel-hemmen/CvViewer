# CvViewer

A full-stack application for viewing CVs, built with .NET and Angular.

## Architecture

### Backend (.NET 10)

- **Clean Architecture**: Separated into Api, ApplicationServices, DataAccess, and Domain layers.
- **CQRS-lite**: Implemented using MediatR.
- **Database**: Entity Framework Core supporting SQL Server and SQLite.
- **Authentication**: Azure AD (JwtBearer).

### Frontend (Angular 20)

- **Modern Angular**: Uses Standalone Components and Signals.
- **SSR**: Server-Side Rendering enabled with Node.js/Express.
- **Styling**: SCSS.

## Project Structure

- `backend/`: Contains the .NET solution and source code.
- `frontend/`: Contains the Angular application.

## Getting Started

### Backend

Navigate to the `backend` directory.

Run the API:

```bash
dotnet run --project src/Api/Api.csproj
```

Run tests:

```bash
dotnet test CvViewer.slnx
```

### Frontend

Navigate to the `frontend` directory.

Start the development server:

```bash
npm start
```

The application will be available at `http://localhost:4200`.

Run tests:

```bash
npm run test
```

## Configuration

- **Backend**: Configured via `appsettings.json` and environment variables.
- **Frontend**: Configured via `src/app/app.config.ts` (Browser) and `src/app/app.config.server.ts` (Server).
