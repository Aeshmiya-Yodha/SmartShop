---
title: SmartShop Backend API
description: ASP.NET Core backend for an e-commerce system with product, user, cart, order, and address domain models.
ms.date: 2026-07-24
---

## SmartShop Backend API

SmartShop is a backend API for an e-commerce application built with ASP.NET Core, Entity Framework Core, and PostgreSQL.

The project is being built version by version. The current focus is getting the core backend entities and CRUD operations working correctly, then improving the API with cleaner response models, validation, soft delete behavior, pagination, and authentication.

## Why This Project Exists

SmartShop is a backend project focused on building a well-structured e-commerce API. It demonstrates how real APIs are organized, how Entity Framework Core maps C# models to database tables, and how controllers handle common backend workflows like filtering, creating, updating, and deleting data.

The project is managed with GitHub Issues and a GitHub Project board to follow a professional development workflow.

## Tech Stack

| Area | Technology |
| --- | --- |
| Backend | ASP.NET Core Web API |
| Language | C# |
| ORM | Entity Framework Core |
| Database | PostgreSQL |
| Provider | Npgsql.EntityFrameworkCore.PostgreSQL |
| Project tracking | GitHub Issues + GitHub Projects |

## Domain Model

SmartShop currently has these main entities:

| Entity | Purpose |
| --- | --- |
| Product | Stores product details such as name, price, stock, category, image, and active status |
| ProductCategory | Groups products by category |
| User | Stores customer profile and authentication-related fields |
| Address | Stores user delivery addresses |
| Cart | Represents a user's shopping cart |
| CartItem | Represents one product inside a cart |
| Order | Represents a placed order with status and payment state |
| OrderItem | Represents one product inside an order |

## Current API Progress

| Area | Status |
| --- | --- |
| Product API | Initial CRUD structure added |
| Product filtering | Added filtering by product name and category |
| Product update | In progress |
| Product delete | Hard delete currently exists, soft delete planned |
| User API | Planned |
| Order API | Basic placeholder exists, CRUD planned |
| Cart API | Planned |
| Address API | Planned |

## Product API Endpoints

Current ProductController endpoints:

```http
GET    /Product
GET    /Product/{id}
POST   /Product
PUT    /Product/{id}
DELETE /Product/{id}
```

Current filtering support:

```http
GET /Product?ProductName=Phone
GET /Product?ProductCategory=Electronics
GET /Product?ProductCategory=Electronics&ProductName=Phone
```

## Development Roadmap

The project is being improved in layers.

| Version | Focus |
| --- | --- |
| v1 | Complete basic CRUD operations for the main controllers |
| v2 | Add soft delete and hard delete behavior |
| v3 | Add pagination, filtering, sorting, and search |
| v4 | Improve status codes and response shape |
| v5 | Add DTOs and validation |
| v6 | Add authentication and authorization |
| v7 | Add tests and deployment readiness |

## Design Decisions

Product filtering is built on `IQueryable<Product>` so that filters translate to SQL and run on the database side, instead of loading every row into memory and filtering in C#:

```csharp
IQueryable<Product> query = _context.Products;

// Conditional filters are composed here

List<Product> products = await query.ToListAsync();
```

This keeps queries efficient as the dataset grows.

## Running Locally

Prerequisites:

- .NET SDK installed
- PostgreSQL installed or available remotely
- A valid `DefaultConnection` connection string configured through user secrets or configuration

Restore dependencies:

```bash
dotnet restore
```

Run the API:

```bash
dotnet run
```

The API can be tested using:

- `SmartShop.http`
- Postman
- Thunder Client
- Browser for simple GET endpoints

## Project Management

This repo uses GitHub Issues and a GitHub Project board to track work.

Board flow:

```text
Backlog -> Ready -> In Progress -> Done
```

Current development style:

1. Create an issue for each feature or improvement.
2. Move the issue to In Progress while coding.
3. Commit and push after completing the work.
4. Move the issue to Done when tested.

## Repository Goal

The goal is to grow SmartShop into a clean backend project that demonstrates:

- ASP.NET Core controller design
- Entity Framework Core relationships
- PostgreSQL-backed API development
- CRUD workflows
- Query filtering and pagination
- Professional issue tracking
- Gradual backend improvement through versions
