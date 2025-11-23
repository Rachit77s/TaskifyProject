# 🎯 Taskify - Task Management Application

A full-stack task management application built with .NET 10 (Backend) and React (Frontend) featuring JWT authentication, user-specific task isolation, and comprehensive CRUD operations.

---

## 🏗️ **System Design Overview**

### **Architecture**
```
┌─────────────────┐         ┌──────────────────┐         ┌─────────────────┐
│                 │         │                  │         │                 │
│  React Frontend │◄───────►│  .NET 10 Web API │◄───────►│  SQL Server DB  │
│  (Port 3000)    │  HTTP   │  (Port 5226)     │  EF     │   (LocalDB)     │
│                 │◄───────►│                  │◄───────►│                 │
└─────────────────┘  JSON   └──────────────────┘ Core    └─────────────────┘
        │                            │
        │                            │
        │                            │
   JWT Token                   JWT Validation
   Storage &                   Middleware &
   Auto-Include               Authorization
```

### **Backend Architecture (Layered Design)**
```
┌──────────────────────────────────────────────────────────┐
│                         Controllers                          │
│  (API Endpoints - TasksController, AuthController)          │
└──────────────────────────────────────────────────────────┘
                             │
┌──────────────────────────────────────────────────────────┐
│                          Services                            │
│  (Business Logic - TaskService, AuthService)                 │
└──────────────────────────────────────────────────────────┘
                             │
┌──────────────────────────────────────────────────────────┐
│                        Repositories                          │
│  (Data Access - TaskRepository)                              │
└──────────────────────────────────────────────────────────┘
                             │
┌──────────────────────────────────────────────────────────┐
│                       DbContext (EF Core)                    │
│  (ApplicationDbContext)                                      │
└──────────────────────────────────────────────────────────┘
                             │
                    ┌─────────────────┐
                    │  SQL Server DB  │
                    │   (TaskifyDb)   │
                    └─────────────────┘
```

### **Database Schema**
```
┌─────────────────┐         ┌──────────────────┐
│     Users       │         │      Tasks       │
├─────────────────┤         ├──────────────────┤
│ Id (PK)         │◄───┐    │ Id (PK)          │
│ Username        │     │    │ Title            │
│ Email           │     │    │ Description      │
│ PasswordHash    │     └────│ UserId (FK)      │
│ FirstName       │          │ DueDate          │
│ LastName        │          │ Priority         │
│ Role            │          │ Status           │
│ IsActive        │          │ CreatedAt        │
│ CreatedAt       │          │ UpdatedAt        │
│ UpdatedAt       │          └──────────────────┘
└─────────────────┘
   1       :       N
   (One User has Many Tasks)
   Cascade Delete Enabled
```

### **Key Design Patterns**
- **Repository Pattern** - Data access abstraction
- **Service Layer Pattern** - Business logic separation
- **DTO Pattern** - Data transfer optimization
- **Dependency Injection** - Loose coupling
- **Middleware Pattern** - Cross-cutting concerns
- **Token-Based Authentication** - Stateless security

---

## 🚀 **Setup Instructions**

### **Prerequisites**
- **.NET 10 SDK** - [Download](https://dotnet.microsoft.com/download/dotnet/10.0)
- **Node.js 18+** - [Download](https://nodejs.org/)
- **SQL Server LocalDB** - Included with Visual Studio or install separately
- **Git** (optional) - For cloning the repository

### **Backend Setup**

#### **Step 1: Navigate to Backend Directory**
```bash
cd TaskifyProject
```

#### **Step 2: Restore NuGet Packages**
```bash
dotnet restore
```

#### **Step 3: Update Database Connection (Optional)**
Edit `appsettings.json` if you want to use a different SQL Server instance:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TaskifyDb;..."
  }
}
```

#### **Step 4: Create Database and Apply Migrations**
```bash
dotnet ef database update
```

This will:
- Create `TaskifyDb` database
- Create `Users` and `Tasks` tables
- Apply all migrations
- Set up indexes and relationships

#### **Step 5: Run the Backend**
```bash
dotnet run
```

? **Backend is now running on:** `http://localhost:5226`

**Verify:** Open `http://localhost:5226/` in your browser to see Swagger UI

#### **Step 6: Database Auto-Seeding**
On first run, the database will automatically seed with:
- **3 test users** (rachit, testuser, demo)
- **14 sample tasks** (6, 4, 4 distribution)

---

### **Frontend Setup**

#### **Step 1: Navigate to Frontend Directory**
```bash
cd ../taskify-frontend
```

#### **Step 2: Install Dependencies**
```bash
npm install
```

This will install:
- React 18
- React Router v6
- Axios
- React Icons
- date-fns

#### **Step 3: Update API URL (Optional)**
Edit `src/services/taskService.js` if backend is on different URL:
```javascript
const API_BASE_URL = 'http://localhost:5226/api';
```

#### **Step 4: Start the Frontend**
```bash
npm start
```

? **Frontend is now running on:** `http://localhost:3000`

The browser will automatically open to the login page.

---

### **Quick Test**

1. **Login** with test credentials:
   ```
   Username: rachit
   Password: Admin@123
   ```

2. **You should see:**
   - 6 tasks for Rachit
   - User info in header
   - Fully functional task management

3. **Try these actions:**
   - ? Create a new task
   - ? Edit existing task
   - ? Toggle task status
   - ? Filter by priority/status
   - ? Delete a task
   - ? Logout and login

---

## ?? **Test Credentials**

| Username | Password | Role | Tasks | Use Case |
|----------|----------|------|-------|----------|
| rachit | Admin@123 | Admin | 6 | Full dataset testing |
| testuser | Test@123 | User | 4 | Regular user testing |
| demo | Demo@123 | User | 4 | Demo/presentations |

---

## ?? **API Documentation**

### **Swagger UI**
**URL:** `http://localhost:5226/`

### **Authentication Endpoints** (Public)
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login and get JWT token
- `POST /api/auth/validate` - Validate token

### **Task Endpoints** (Protected - Requires JWT)
- `POST /api/tasks` - Create task
- `GET /api/tasks` - Get all user's tasks
- `GET /api/tasks/{id}` - Get task by ID
- `GET /api/tasks/filter` - Get filtered tasks (status, priority, pagination)
- `PUT /api/tasks/{id}` - Update task
- `DELETE /api/tasks/{id}` - Delete task

### **Using Swagger**
1. Open `http://localhost:5226/`
2. Click `POST /api/auth/login`
3. Try it out with test credentials
4. Copy the `token` from response
5. Click **"Authorize"** button (?? icon)
6. Enter: `Bearer <your-token>`
7. Click "Authorize" then "Close"
8. Now you can test all protected endpoints!

### **Using Postman**
1. Import the collection: `Taskify_API.postman_collection.json`
2. The collection includes all endpoints with examples
3. Login requests auto-save JWT token to collection variable
4. All task requests auto-use saved token

---

## ??? **Technology Stack**

### **Backend**
- **.NET 10** - Latest .NET framework
- **ASP.NET Core Web API** - RESTful API
- **Entity Framework Core 10** - ORM
- **SQL Server LocalDB** - Database
- **JWT Authentication** - Secure token-based auth
- **BCrypt.NET** - Password hashing (12 rounds)
- **Swashbuckle** - Swagger/OpenAPI documentation

### **Frontend**
- **React 18** - UI framework
- **React Router v6** - Client-side routing
- **Axios** - HTTP client
- **React Icons** - Icon library
- **date-fns** - Date formatting

---

## ?? **Project Structure**

```
TaskifyProject/                    # Backend
??? Controllers/                   # API Controllers
?   ??? AuthController.cs         # Authentication endpoints
?   ??? TasksController.cs        # Task CRUD endpoints
??? Data/                         # Database context & seeding
?   ??? ApplicationDbContext.cs  # EF Core DbContext
?   ??? DatabaseSeeder.cs        # Seed initial data
??? Middleware/                   # Custom middleware
?   ??? GlobalExceptionMiddleware.cs
??? Models/
?   ??? Configuration/            # App configuration
?   ?   ??? JwtSettings.cs
?   ??? DTOs/                     # Data transfer objects
?   ?   ??? Auth/                # Auth DTOs
?   ?   ??? Common/              # Shared DTOs
?   ?   ??? Tasks/               # Task DTOs
?   ??? Entities/                 # Database entities
?   ?   ??? User.cs
?   ?   ??? TaskItem.cs
?   ??? Enums/                    # Enumerations
?       ??? TaskPriority.cs
?       ??? TaskStatus.cs
??? Repositories/                 # Data access layer
?   ??? ITaskRepository.cs
?   ??? TaskRepository.cs
??? Services/                     # Business logic
?   ??? IAuthService.cs
?   ??? AuthService.cs
?   ??? ITaskService.cs
?   ??? TaskService.cs
??? appsettings.json             # Configuration
??? Program.cs                   # App entry point

taskify-frontend/                 # Frontend
??? src/
?   ??? components/              # React components
?   ?   ??? FilterBar.js
?   ?   ??? ProtectedRoute.js
?   ?   ??? TaskForm.js
?   ?   ??? TaskList.js
?   ??? context/                 # React Context
?   ?   ??? AuthContext.js      # Auth state management
?   ??? pages/                   # Page components
?   ?   ??? Dashboard.js        # Main app
?   ?   ??? Login.js
?   ?   ??? Register.js
?   ??? services/                # API services
?   ?   ??? taskService.js      # API calls
?   ??? App.css
?   ??? App.js                  # Main app with routing
?   ??? index.js                # App entry point
??? package.json                # Dependencies
```

---

## ?? **Security Features**

- ? **JWT Token Authentication** - Secure, stateless authentication
- ? **BCrypt Password Hashing** - 12 rounds of hashing
- ? **User-Specific Data Isolation** - Users can only access their own data
- ? **Protected API Endpoints** - All task endpoints require authentication
- ? **Role-Based Authorization** - Admin and User roles
- ? **CORS Configuration** - Controlled cross-origin requests
- ? **Token Expiration** - 60-minute token lifetime
- ? **Auto Logout on Expiry** - Frontend handles 401 errors
- ? **Unique Constraints** - Username and Email uniqueness enforced
- ? **Cascade Delete** - User deletion removes their tasks

---

## ?? **Key Features**

### **Backend Features**
- ? Complete CRUD operations for tasks
- ? JWT authentication and authorization
- ? User registration and login
- ? Password hashing with BCrypt
- ? User-specific task management
- ? Filtering by status and priority
- ? Pagination support
- ? Global exception handling
- ? Database seeding with test data
- ? Swagger API documentation
- ? RESTful API design
- ? Layered architecture
- ? Repository pattern
- ? Dependency injection

### **Frontend Features**
- ? User authentication (login/register)
- ? Protected routes
- ? Task CRUD operations
- ? Filter by status and priority
- ? Responsive design
- ? User information display
- ? Logout functionality
- ? Beautiful gradient UI
- ? Loading states
- ? Error handling
- ? Form validation
- ? Auto token management
- ? Session persistence

---

## ?? **Testing**

### **Test with Swagger:**
1. Start backend (`dotnet run`)
2. Open `http://localhost:5226/`
3. POST `/api/auth/login` with test credentials
4. Copy the JWT token from response
5. Click "Authorize" button (??)
6. Enter `Bearer <your-token>`
7. Test all protected endpoints

### **Test with Postman:**
1. Import `Taskify_API.postman_collection.json`
2. Execute login request (token auto-saves)
3. Execute other requests (token auto-applies)

### **Test with Frontend:**
1. Start both backend and frontend
2. Open `http://localhost:3000/`
3. Login with test credentials
4. Create, view, edit, delete tasks
5. Test filters and status toggle
6. Test logout

---

## ?? **Database Reset**

If you need to reset the database:

```bash
cd TaskifyProject
dotnet ef database drop --force
dotnet ef database update
dotnet run
```

Database will auto-seed with 3 users and 14 tasks.

---

## ?? **Troubleshooting**

**Backend not starting?**
- Ensure .NET 10 SDK is installed (`dotnet --version`)
- Check SQL Server LocalDB is running
- Verify port 5226 is available
- Check `appsettings.json` connection string

**Frontend not starting?**
- Run `npm install` first
- Check port 3000 is available
- Clear `node_modules` and reinstall if issues persist
- Verify Node.js version (`node --version`)

**Can't login?**
- Ensure backend is running on port 5226
- Check browser console for errors
- Verify CORS is configured correctly
- Ensure database is seeded (check backend logs)

**Database errors?**
- Run `dotnet ef database drop --force`
- Run `dotnet ef database update`
- Restart the backend

**Token expired?**
- Login again to get new token
- Default expiration is 60 minutes
- Frontend auto-redirects to login on 401

---

## ?? **License**

This project is for educational purposes.

---

**Built with ?? using .NET 10 and React**
