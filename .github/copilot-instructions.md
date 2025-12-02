# Copilot instructions — CvViewer

This repository contains a full-stack application with an Angular frontend and a .NET backend.

## Project Structure

- **Frontend**: `frontend/` (Angular 20, Standalone, SSR)
- **Backend**: `backend/` (.NET 10, Clean Architecture, MediatR)

## Backend (`backend/`)

### Architecture & Patterns

- **Clean Architecture**:
  - `src/Api`: Entry point, Controllers (organized by Feature), Configuration.
  - `src/ApplicationServices`: Business logic, MediatR Handlers (`Handlers/`), Requests (`Requests/`).
  - `src/DataAccess`: EF Core `CvContext`, Repositories, Migrations.
  - `src/Domain`: Pure domain entities and logic.
- **CQRS-lite**: Uses **MediatR**. Controllers create `Request` objects -> dispatched to `Handlers` in `ApplicationServices`.
- **Feature Organization**:
  - API Controllers are in `src/Api/Features/<FeatureName>`.
  - Handlers are in `src/ApplicationServices/Handlers/<FeatureName>`.

### Key Technologies

- **Framework**: .NET 10.0 (ASP.NET Core Web API).
- **Database**: EF Core with SQL Server (Production) and SQLite (Dev/Test).
- **Date/Time**: Uses **NodaTime** (`Instant`, `LocalDate`) instead of `DateTime`.
- **Auth**: Azure AD (JwtBearer).

### Development Workflows

- **Run API**: `dotnet run --project src/Api/Api.csproj`
- **Run Tests**: `dotnet test CvViewer.slnx`
- **Database**: Migrations are applied automatically in non-Development environments.
- **Configuration**: `appsettings.json` and Environment Variables (`AZURE_SQL_CONNECTIONSTRING`, `BLOB_STORAGE_URL`).

### Coding Conventions

- **Dependency Injection**: Use extension methods (e.g., `AddApplicationServices`, `AddDataAccessServices`) to register layer-specific services.
- **Validation**: Check `DataAccess` or `ApplicationServices` for validation logic (FluentValidation is present).
- **DTOs**: Keep DTOs separate from Domain Entities.

## Frontend (`frontend/`)

### Architecture & Patterns

- **Framework**: Angular 20 with **Standalone Components** and **Signals**. No NgModules.
- **SSR**: Uses `@angular/ssr` with a Node.js/Express server (`src/server.ts`).
- **Routing**: Defined in `src/app/app.routes.ts` (Client) and `src/app/app.routes.server.ts` (Server). **Update both** when adding routes.
- **Config**: `src/app/app.config.ts` (Browser) and `src/app/app.config.server.ts` (Server).

### Key Files

- `src/main.ts`: Client bootstrap.
- `src/main.server.ts`: Server bootstrap.
- `src/app/app.ts`: Root component (example of Standalone pattern).
- `public/data/cv-details.json`: Static sample data.

### Development Workflows

- **Dev Server**: `npm start` or `ng serve` (Port 4200).
- **Watch Build**: `npm run watch`.
- **SSR Test**: `npm run build` then `npm run serve:ssr:cv-viewer`.
- **Test**: `npm run test` (Karma/Jasmine).

### Coding Conventions

- **Styling**: SCSS everywhere. Use `styleUrl: './component.scss'`.
- **HTTP**: Use `HttpClient` with `withFetch()` (configured in `app.config.ts`).
- **Imports**: Explicitly list imports in `@Component({ imports: [...] })`.

## Integration & Cross-Cutting

- **CORS**: Backend `Program.cs` allows `localhost:4200` and specific Azure Static Web App URLs.
- **Authentication**: Frontend authenticates with Azure AD; Backend validates tokens via `Microsoft.Identity.Web`.
- **Shared Concepts**: `cv-details.json` is used as a seeder in Backend (`DataAccess/Seeder`) and sample data in Frontend (`public/data`).

## General Guidelines

- **Comments**: Do NOT add comments to code unless the logic is extremely non-obvious or explicitly requested by the user.
