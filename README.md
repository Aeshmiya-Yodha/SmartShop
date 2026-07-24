---
title: SmartShop Backend API
description: ASP.NET Core e-commerce backend API built with Entity Framework Core and PostgreSQL.
ms.date: 2026-07-24
---

## SmartShop Backend API

SmartShop is a backend API for an e-commerce application built with ASP.NET Core, Entity Framework Core, and PostgreSQL. It exposes REST endpoints for managing products, categories, users, carts, orders, and addresses.

## Purpose

SmartShop provides the server-side foundation of an online store. It models the data and relationships behind common e-commerce actions: browsing and filtering products, managing a shopping cart, placing orders, and storing customer and address details. The code is organized around clean ASP.NET Core controllers and Entity Framework Core entities backed by a PostgreSQL database.

## Tech Stack

| Area | Technology |
| --- | --- |
| Backend | ASP.NET Core Web API |
| Language | C# |
| ORM | Entity Framework Core |
| Database | PostgreSQL |
| Provider | Npgsql.EntityFrameworkCore.PostgreSQL |

## Domain Model

| Entity | Purpose |
| --- | --- |
| Product | Stores product details such as name, price, stock, category, image, and active status |
| ProductCategory | Groups products by category |
| User | Stores customer profile and account details |
| Address | Stores user delivery addresses |
| Cart | Represents a user's shopping cart |
| CartItem | Represents one product inside a cart |
| Order | Represents a placed order with status and payment state |
| OrderItem | Represents one product inside an order |

## Product API Endpoints

| Method | Route | Description |
| --- | --- | --- |
| GET | `/Product` | List products, with optional filtering |
| GET | `/Product/{id}` | Get a product by id |
| POST | `/Product` | Create a product |
| PUT | `/Product/{id}` | Update a product |
| DELETE | `/Product/{id}` | Delete a product |

Filtering is supported through query parameters:

```http
GET /Product?ProductName=Phone
GET /Product?ProductCategory=Electronics
GET /Product?ProductCategory=Electronics&ProductName=Phone
```

## Design Notes

Product queries are built on `IQueryable<Product>` so that filters translate to SQL and run on the database side, instead of loading every row into memory and filtering in C#:

```csharp
IQueryable<Product> query = _context.Products;

// Conditional filters are composed here

List<Product> products = await query.ToListAsync();
```

This keeps queries efficient as the dataset grows.

## Getting Started

### Prerequisites

- .NET SDK
- PostgreSQL (local or remote)

### 1. Configure the database connection

The API reads a connection string named `DefaultConnection`. Store it with user secrets so it stays out of source control:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=smartshop;Username=postgres;Password=your_password"
```

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Create the database schema

If the EF Core tools are not installed yet:

```bash
dotnet tool install --global dotnet-ef
```

Generate the initial migration and apply it to the database:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. Run the API

```bash
dotnet run
```

## Testing the API

With the API running, call the endpoints using any of these:

- `SmartShop.http` in the repository
- Postman or Thunder Client
- A browser for simple GET requests
