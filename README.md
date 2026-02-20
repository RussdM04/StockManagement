# StockManagement — ASP.NET Core MVC + SQL Server (EF Core)

A simple stock/inventory management web app built with **ASP.NET Core MVC (.NET 8)**, **Entity Framework Core**, and **SQL Server (SSMS)**.  
Includes **role-based access** with two user types: **Manager** and **User**.

---

## Features

- Inventory / stock CRUD (create, view, edit, delete)
- SQL Server database with EF Core migrations
- MVC architecture (Controllers / Models / Views)
- Session-based login
- Role-based access:
  - **Manager**: full access (manage inventory + admin actions)
  - **User**: limited access (typically view / basic actions)

> ⚠️ Security note: Authentication is implemented for demo/learning purposes and may use non-production password handling.

---

## Tech Stack

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core (Migrations)
- SQL Server / LocalDB (SSMS)
- Visual Studio 2022
