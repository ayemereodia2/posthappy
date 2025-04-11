# Blog API

A clean and scalable **ASP.NET Core Web API** for managing blog posts. This API allows users to create, update, delete, and retrieve blog posts efficiently.

## 🚀 Features
- RESTful API endpoints
- Clean architecture with **separation of concerns**
- Supports both **Entity Framework Core** and **ADO.NET** repositories
- **Dependency Injection** for testability
- **Swagger UI** for easy API exploration
- **EF Core Migrations** for database schema management

---

## 📂 Project Structure

```
my-blog-api/
│── BlogApp.API/        # Main API project (Controllers, Middleware)
│── BlogCore/           # Core business logic (Services, Entities, Interfaces)
│── BlogInfrastructure/ # Data access (Repositories, DbContext, Migrations)
│── BlogApp.Tests/      # Unit tests (NUnit, Moq)
│── README.md          # Project documentation
│── BlogApp.sln        # Solution file
```

---

## ⚙️ Setup & Installation

### 1️⃣ Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio Code](https://code.visualstudio.com/) (or Visual Studio)

### 2️⃣ Clone the Repository
```sh
git clone https://github.com/yourusername/my-blog-api.git
cd my-blog-api
```

### 3️⃣ Configure the Database
Update **appsettings.json** in `BlogApp.API`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=your-server;Database=BlogDB;User Id=your-user;Password=your-password;TrustServerCertificate=True;"
}
```

### 4️⃣ Run Database Migrations
```sh
dotnet ef migrations add InitialCreate --project BlogInfrastructure --startup-project BlogApp.API

dotnet ef database update --project BlogInfrastructure --startup-project BlogApp.API
```

### 5️⃣ Run the API
```sh
dotnet run --project BlogApp.API
```
API will be available at `http://localhost:5000` (or `https://localhost:5001`).

---

## 🔥 API Endpoints

### 📌 Blog Endpoints
| Method | Endpoint            | Description                 |
|--------|---------------------|-----------------------------|
| `POST` | `/api/blogs`        | Create a new blog post     |
| `GET`  | `/api/blogs`        | Get all blog posts         |
| `GET`  | `/api/blogs/{id}`   | Get a blog post by ID      |
| `PUT`  | `/api/blogs/{id}`   | Update a blog post        |
| `DELETE` | `/api/blogs/{id}` | Delete a blog post        |

📖 **Swagger UI** available at: `http://localhost:5000/swagger`

---

## 🛠️ Technologies Used
- **ASP.NET Core 8 Web API**
- **Entity Framework Core** (with SQL Server)
- **ADO.NET** (alternative repository option)
- **NUnit & Moq** (for unit testing)
- **Swagger UI** (for API documentation)

---

## ✅ Testing
Run unit tests using:
```sh
dotnet test
```

---

## 📜 License
This project is licensed under the **MIT License**.

---

## 💡 Future Enhancements
- 📝 Implement JWT authentication for secure API access
- 🏷️ Add category and tagging support for blogs
- 🛠️ Integrate caching for performance optimization

Feel free to contribute by submitting a pull request! 🚀

