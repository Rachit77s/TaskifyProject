# 🎯 TaskifyProject

A full-stack task management application featuring a **React frontend** and **ASP.NET Core backend** with JWT authentication, real-time task management, and comprehensive filtering capabilities.

![React](https://img.shields.io/badge/React-19.2.0-61DAFB?logo=react)
![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)
![SQL Server](https://img.shields.io/badge/SQL_Server-LocalDB-CC2927?logo=microsoftsqlserver)

---

## 📁 Project Structure

This monorepo contains both the frontend and backend applications:

```
TaskifyProject/
├── taskify-frontend/          # React application
│   ├── src/
│   ├── public/
│   ├── package.json
│   └── README.md             # Frontend documentation
│
├── TaskifyProject/            # ASP.NET Core Web API
│   ├── Controllers/
│   ├── Models/
│   ├── Services/
│   ├── Repositories/
│   ├── Data/
│   ├── Migrations/
│   └── README.md             # Backend documentation
│
└── README.md                 # This file
```

---

## ✨ Key Features

### 🔐 Authentication & Security
- JWT-based authentication
- Secure password hashing with BCrypt
- Protected API endpoints
- User-specific task isolation

### 📝 Task Management
- Create, read, update, and delete tasks
- Task priorities (Low, Medium, High)
- Task statuses (To-Do, In Progress, Completed)
- Due date tracking
- Advanced filtering and search

### ️ Architecture
- RESTful API design
- Layered architecture (Controllers → Services → Repositories)
- Entity Framework Core with Code-First migrations
- Context API for state management (Frontend)
- Global error handling middleware

> 📖 **For detailed feature descriptions and architecture details:**
> - Frontend features: [taskify-frontend/README.md](taskify-frontend/README.md)
> - Backend architecture: [TaskifyProject/README.md](TaskifyProject/README.md)

---

## 🚀 Quick Start

### Prerequisites

- **Node.js** (v16 or higher) - [Download here](https://nodejs.org/)
- **.NET SDK** (10.0 or higher) - [Download here](https://dotnet.microsoft.com/download)
- **SQL Server** (LocalDB or full instance)
- **Git**

### 1️⃣ Clone the Repository

```bash
git clone https://github.com/Rachit77s/TaskifyProject.git
cd TaskifyProject
```

---

### 2️⃣ Backend Setup (.NET Web API)

Navigate to the backend folder and follow these steps:

```bash
# Navigate to backend
cd TaskifyProject

# Restore NuGet packages
dotnet restore

# Apply database migrations
dotnet ef database update

# Run the backend API
dotnet run
```

✅ **Backend is now running at:** `https://localhost:5226` or `http://localhost:5000`

> 📖 **For detailed backend setup, API documentation, and troubleshooting:**  
> See [TaskifyProject/README.md](TaskifyProject/README.md) or [TaskifyProject/QUICKSTART.md](TaskifyProject/QUICKSTART.md)

**Note:** The default connection string uses SQL Server LocalDB. If you need to change it, edit `appsettings.json`.

---

### 3️⃣ Frontend Setup (React App)

Open a **new terminal** and navigate to the frontend folder:

```bash
# Navigate to frontend (from project root)
cd taskify-frontend

# Install npm dependencies
npm install

# Start the React development server
npm start
```

✅ **Frontend is now running at:** `http://localhost:3000`

> 📖 **For detailed frontend setup, features, and configuration:**  
> See [taskify-frontend/README.md](taskify-frontend/README.md)

**Note:** The app will use `http://localhost:5226/api` by default. To change the backend URL, create a `.env` file and set `REACT_APP_API_BASE_URL`.

---

### 4️⃣ Verify Setup

1. **Backend**: Open `https://localhost:5226/swagger` to see API documentation
2. **Frontend**: Open `http://localhost:3000` to access the application
3. **Test**: Register a new user and create your first task!

---

## 🛠️ Technology Stack

### Frontend
- **React 19.2.0** - UI framework
- **Axios** - HTTP client
- **Context API** - State management
- **React Icons** - Icon library
- **date-fns** - Date manipulation
- **CSS3** - Modern styling

### Backend
- **.NET 10** - Web API framework
- **Entity Framework Core** - ORM
- **SQL Server** - Database
- **JWT Authentication** - Security
- **BCrypt.Net** - Password hashing
- **Swagger/OpenAPI** - API documentation

---

## 🔗 API Endpoints

### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - User login

### Tasks
- `GET /api/tasks` - Get all tasks (with optional filters)
- `GET /api/tasks/{id}` - Get task by ID
- `POST /api/tasks` - Create new task
- `PUT /api/tasks/{id}` - Update task
- `DELETE /api/tasks/{id}` - Delete task

📖 **Full API documentation available at:** `https://localhost:5226/swagger` (when backend is running)

---

## 📊 Database Schema

### Users Table
- `UserId` (Primary Key)
- `Username` (Unique)
- `Email` (Unique)
- `PasswordHash`
- `CreatedAt`

### TaskItems Table
- `TaskId` (Primary Key)
- `Title`
- `Description`
- `Status` (To-Do, In Progress, Completed)
- `Priority` (Low, Medium, High)
- `DueDate`
- `UserId` (Foreign Key)
- `CreatedAt`
- `UpdatedAt`

---

## 📚 Additional Documentation

- **Frontend Details:** [taskify-frontend/README.md](taskify-frontend/README.md)
- **Backend Details:** [TaskifyProject/README.md](TaskifyProject/README.md)
- **Backend Quick Start:** [TaskifyProject/QUICKSTART.md](TaskifyProject/QUICKSTART.md)

---

## 🎯 Project Highlights

✅ Full-stack application with modern tech stack  
✅ Secure JWT-based authentication  
✅ Clean architecture with separation of concerns  
✅ RESTful API design  
✅ Responsive and modern UI  
✅ Database migrations for easy deployment  
✅ Comprehensive error handling  
✅ User-specific data isolation  
✅ API documentation with Swagger  
✅ Production-ready code structure  

---

**Happy Coding! 🚀**