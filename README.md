# 📝 Blog Hub API - ASP.NET Core Web API

A robust, secure, and fully documented RESTful API for a Blogging platform. Built with **ASP.NET Core 8.0**, this project features advanced security logic, JWT authentication, and a clean Service-Repository architecture.

## 🚀 Key Features
* **Secure Authentication**: Fully implemented JWT (JSON Web Token) authentication.
* **Smart Authorization**: Users can only **Update** or **Delete** their own posts and accounts.
* **Account Safety**: Automatic blocking of actions if an account is deleted, even if the Token is still valid.
* **Relational Integrity**: Comprehensive management of Users, BlogPosts, Categories, and Comments.
* **Documentation**: Interactive API testing via **Swagger UI**.

---

## 🛠️ Tech Stack
* **Framework**: .NET 8.0 (ASP.NET Core)
* **Database**: Entity Framework Core (SQL Server)
* **Security**: JWT Authentication, Password Hashing
* **Architecture**: Service-Repository Pattern
* **Documentation**: Swagger (Swashbuckle)

---

## 🔑 Security & Logic Highlights
This project meets high-security standards  by implementing:

1.  **Implicit User Identification**: For user-specific actions (like updating profile or deleting account), the system identifies the user via the **JWT Claim**, eliminating the need to pass `userId` in the request body for better security.
2.  **Strict Ownership Verification**: Before any `Update` or `Delete` of a post, the system performs a three-step check:
    * **Authentication**: Is the user logged in? ($401$)
    * **Existence**: Does the post/account exist? ($404$)
    * **Ownership**: Does the post belong to the current user? ($403$)
3.  **Active Account Verification**: Every authorized request verifies that the user account still exists in the database. This prevents "Ghost Tokens" (valid tokens belonging to deleted accounts) from performing actions.

---

## 📡 API Endpoints

### 👤 User Management
* `POST /api/User/Register` - Create a new account.
* `POST /api/User/Login` - Authenticate and receive a JWT Token.
* `PUT /api/User/Update` - Update current user profile (identified by Token).
* `DELETE /api/User/Delete` - Delete current user account (identified by Token).

### 📝 Blog Posts
* `GET /api/BlogPost/All` - Retrieve all blog posts.
* `GET /api/BlogPost/Search` - Search posts by title or content.
* `GET /api/BlogPost/Filter` - Filter posts by Category.
* `POST /api/BlogPost/Create` - Create a new post (Auto-linked to logged-in user).
* `PUT /api/BlogPost/Update/{id}` - Update a specific post (Ownership required).
* `DELETE /api/BlogPost/Delete/{id}` - Delete a specific post (Ownership required).

### 🏷️ Categories & 💬 Comments
* `GET /api/Category/All` - List all available categories.
* `POST /api/Comment/Create` - Post a comment on a blog post.

---

## 🏃 Getting Started

1.  **Clone the Repository**:
    ```bash
    git clone https://github.com/Qian1507/BloggCommunityAPI.git
2.  **Update Database Connection**:
    Update the `ConnectionStrings` in `appsettings.json` to point to your local SQL Server instance.
3.  **Apply Migrations**:
    ```bash
    dotnet ef database update
    ```
4.  **Run the Project**:
    ```bash
    dotnet run
    ```
5.  **Test the API**:
    Open `https://localhost:[port]/swagger` in your browser to explore the API.

---

## 📂 Architecture Overview
The project follows a clean separation of concerns:
* **Controllers**: Handle HTTP requests and map status codes ($200, 204, 401, 403, 404$).
* **Services**: Encapsulate business logic and permission checks.
* **Repositories**: Manage data persistence using Entity Framework.
* **DTOs**: Ensure clean data transfer and hide sensitive database structures.

---

## 🧑‍💻 Author
Developed as part of the Web Development Course Requirement.




    
