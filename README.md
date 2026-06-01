# 💰 ExpenseManager-CoreMVC

[![Framework](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![Language](https://img.shields.io/badge/Language-C%23-blue.svg)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![Database](https://img.shields.io/badge/Database-SQL_Server-red.svg)](https://www.microsoft.com/en-us/sql-server/)
[![Architecture](https://img.shields.io/badge/Architecture-Repository_Pattern-orange.svg)]()

A secure, production-ready multi-user personal finance and ledger tracking application built from scratch using **ASP.NET Core MVC**. Designed with robust data isolation and strict clean code principles, the platform allows individuals to register, manage assets, log multi-category financial records, and monitor real-time wallet health through a dynamic analytical dashboard.

---

## 📌 Project Overview
Unlike boilerplate solutions relying on heavy automated identity frameworks, this project builds vital web application components from the ground up. It implements an enterprise-grade **Repository Pattern** decoupled from controllers, a secure custom **SHA256 password cryptography** infrastructure, and a native claims-based cookie session lifecycle handler to demonstrate solid fundamentals in modern backend architecture.

---

## 🚀 Key Features

* **📊 Live Analytics Dashboard:** Aggregates real-time monetary inflows and outgoing costs, displaying dynamic financial health statuses and automated deficit warnings.
* **🔒 Custom Claims-Based Cookie Authentication:** Implements custom secure login sessions utilizing native encrypted HTTP cookie claims instead of heavy out-of-the-box framework libraries.
* **🛡️ Secure Password Hashing:** Utilizes a cryptographic helper utilizing the modern SHA256 engine to execute irreversible hash conversions on input strings before database entry.
* **🧱 Full-Circle CRUD Subsystems:** Complete isolated management engines for handling both **Income** and **Expense** ledgers with form-level data verification.
* **🎨 Dynamic Layout Execution:** Intercepts runtime route context (`ViewContext.RouteData`) inside master wrappers to generate automated navigation state highlighters and premium layout adjustments.

---

## 🏗️ Architectural Pillars & Design Choices

### 1. The Repository Pattern
To adhere to standard **SOLID design guidelines**, data access loops are fully abstracted behind interfaces (`IUserRepository`, `IExpenseRepository`, `IIncomeRepository`). Controllers depend strictly on abstraction contracts rather than direct Entity Framework DB Context pools, rendering the system highly unit-testable and maintainable.

### 2. Strict Multi-User Data Isolation
Security tokens and record query constraints are bound tightly to security claims. When an authorized user interacts with ledger services, the current session identifier (`ClaimTypes.NameIdentifier`) is injected directly into LINQ lookup paths, guaranteeing absolute record boundaries across data entities.

### 3. Safe Destructive Request Pipelines
All write, modify, and delete processing pipelines run strictly under verified HTTP `POST` protocols backed by explicit cross-site forgery token blockers (`[ValidateAntiForgeryToken]`). This design eliminates data security vulnerability points exposed by basic anchor `GET` requests.

---

## 🛠️ Tech Stack

* **Backend Engine:** C# (.NET 8.0), ASP.NET Core MVC
* **Data Persistence:** Entity Framework Core (Code-First), SQL Server (LocalDB)
* **Authentication Security:** Custom Cookie Authentication, Claims-Identity Architecture
* **Frontend Design:** Razor Views HTML5, CSS3 Variables, Bootstrap 5, Bootstrap Icons, jQuery Validation

---

## 📂 Project Structure Map

```text
Expense_Manager/
├── Controllers/            # Request processing & security middleware mapping
├── DataAccess/
│   ├── Entities/           # Relational Code-First Database Models (User, Expense, Income, Category)
│   ├── Repositories/       # Data Access Layer Abstractions & Decoupled Implementations
│   └── ApplicationDbContext.cs
├── Models/                 # Secure ViewModels protecting core DB entities from UI exposure
├── Utilities/              # SHA256 Cryptographic Helper Utilities
├── Views/                  # Razor Presentation files (Dashboard, Account, Ledger Pages)
└── Program.cs              # DI service lifetimes Configuration & HTTP Middleware pipeline
