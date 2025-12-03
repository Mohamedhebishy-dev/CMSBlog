# Historical Blog CMS

A professional Content Management System (CMS) designed for a historical blog, built using **ASP.NET Core MVC** following **Clean Architecture** principles to ensure scalability, maintainability, and high performance.

---

## 🚀 Project Overview

This project provides a robust platform for managing historical blog articles, categories, comments, and media files. It enables administrators and content creators to efficiently manage content while ensuring a smooth user experience.

The project is organized into clear layers according to **Clean Architecture**:

- **Domain**: Contains core models, entities, migrations, and repository implementations.
- **Bl (Business Layer)**: Includes services, business logic, and database context.
- **CMSBlog (Presentation Layer)**: ASP.NET Core MVC project with controllers, views, and view models.
- **API**: API layer for exposing endpoints.
- **Contracts** & **Services**: Interfaces and service implementations.

---

## 🧩 Key Features

- Full article management (create, update, delete, archive, view).
- Category and subcategory support.
- Nested threaded comments.
- Image and media file uploads.
- User-friendly admin dashboard.
- Fully Arabic interface support.
- SEO optimized.
- Clean, scalable architecture.

---

## 🏗️ Technologies Used

- **ASP.NET Core MVC (.NET 8)**
- **Entity Framework Core**
- **Microsoft SQL Server**
- **Repository Pattern + Generic Repository**
- **Dependency Injection**
- **AutoMapper**
- **Bootstrap / Tailwind CSS**

---

## 📂 Project Structure

```
CMSBlog.sln
.gitattributes
.gitignore

API/                    # API layer for endpoints
Bl/                     # Business Logic Layer
    ├── Contracts/      # Interfaces for services and repositories
    ├── Services/       # Service implementations
    └── Bl.csproj
CMSBlog/                # Presentation Layer (ASP.NET Core MVC)
Domain/                 # Domain Models, Migrations, and Repositories
    ├── Migrations/     # EF Core migrations
    ├── Repository/     # Repository implementations
    ├── Domain.csproj
    ├── ApplicationUser.cs
    ├── Article.cs
    ├── BlogContext.cs
    ├── Category.cs
    ├── Comment.cs
    ├── HomePageSettings.cs
    ├── MegaMenuItem.cs
    ├── SecurityBase.cs
    └── SiteSettings.cs
```

---

## 📝 Technical Highlights

- Clean, maintainable code (Clean Code principles).
- SOLID principles applied.
- Separation of concerns with DTOs and ViewModels.
- Combination of 3-Tier Architecture and Clean Architecture.
- Security best practices: input validation, anti-forgery, and sanitization.

---

## 📦 Future Enhancements (Roadmap)

- Advanced roles and permissions system.
- Mobile API integration.
- AI-based content management support.
- Enhanced SEO optimizations.
- Tagging system for articles.

---
