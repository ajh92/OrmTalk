# OrmTalk

A demo freight/logistics load management app built to showcase **Entity Framework Core** patterns and techniques. Used as a companion codebase for a talk on ORMs.

## What It Does

Manages **Customers**, **Loads** (shipments), and **Stops** (pickup/delivery appointments with time windows). Includes a "Hat Winners" feature that identifies customers eligible for a promotional hat based on load volume and hours — demonstrated via a raw SQL CTE mixed with EF Core.

## Architecture

Classic layered architecture targeting **.NET 10**:

```
App (ASP.NET Core MVC)  →  Service (Business Logic)  →  Data (EF Core + SQLite)
                                                              ↓
                                                         TimeUtils (Shared)
```

| Project       | Purpose                                                                                                |
|---------------|--------------------------------------------------------------------------------------------------------|
| **App**       | Controllers, Razor views, view models. Uses [Unpoly](https://unpoly.com/) for progressive enhancement. |
| **Service**   | Business logic behind `ILoadService` and `ICustomerService` interfaces.                                |
| **Data**      | `DbContext`, entity configurations, audit interceptor, migrations, seed data.                          |
| **TimeUtils** | `LocalTimeWindow` value object with NodaTime-backed time zone validation.                              |

## Notable EF Core Patterns

- **Complex properties** — `LocalTimeWindow` mapped as a complex type with 3 columns
- **SaveChanges interceptor** — auto-sets `CreatedAt`/`UpdatedAt` via `IAuditable`
- **Assembly-scanned configurations** — `ApplyConfigurationsFromAssembly` for fluent API configs
- **Raw SQL + EF Core** — CTE query in `GetHatWinnerLoadsAsync()` for the hat winners feature
- **NodaTime integration** — `Instant` and `LocalDateTime` stored natively via SQLite NodaTime provider

## Getting Started

```bash
# Requires .NET 10 SDK
dotnet run --project App
```

The app uses SQLite and auto-migrates on startup in development mode — no database setup needed.

## ⚠️ App Layer Notice

**The `App/` project (controllers, views, and view models) is LLM-generated.** It was produced by a large language model to quickly stand up a working UI over the hand-written Service and Data layers. Review accordingly.
