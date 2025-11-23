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

### 🎨 User Interface
- Modern, responsive design
- Gradient-based UI with smooth animations
- Real-time task updates
- Visual dashboard with statistics
- Mobile-friendly interface

### 🏗️ Architecture
- RESTful API design
- Layered architecture (Controllers → Services → Repositories)
- Entity Framework Core with Code-First migrations
- Context API for state management (Frontend)
- Global error handling middleware

---

## 🚀 Quick Start

### Prerequisites

- **Node.js** (v16 or higher)
- **.NET SDK** (10.0 or higher)
- **SQL Server** (LocalDB or full instance)
- **Git**

### 1️⃣ Clone the Repository

```bash
git clone https://github.com/Rachit77s/TaskifyProject.git
cd TaskifyProject
```

### 2️⃣ Setup Backend

```bash
cd TaskifyProject

# Restore dependencies
dotnet restore

# Update connection string in appsettings.json if needed
# Default uses SQL Server LocalDB

# Apply database migrations
dotnet ef database update

# Run the backend API
dotnet run
```

**Backend will run on:** `https://localhost:5226` or `http://localhost:5000`

📖 **For detailed backend setup and API documentation, see:** [TaskifyProject/README.md](TaskifyProject/README.md)

### 3️⃣ Setup Frontend

```bash
cd ../taskify-frontend

# Install dependencies
npm install

# Create .env file (copy from .env.example)
cp .env.example .env

# Update REACT_APP_API_BASE_URL in .env if needed
# Default: http://localhost:5226/api

# Start the React app
npm start
```

**Frontend will run on:** `http://localhost:3000`

📖 **For detailed frontend setup and features, see:** [taskify-frontend/README.md](taskify-frontend/README.md)

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

## 🧪 Testing

### Backend Testing
```bash
cd TaskifyProject

# Run unit tests (if available)
dotnet test

# Test API endpoints using included files:
# - TaskifyProject.http
# - Taskify_API.postman_collection.json
```

### Frontend Testing
```bash
cd taskify-frontend

# Run tests
npm test

# Run tests with coverage
npm test -- --coverage
```

---

## 📦 Deployment

### Backend Deployment
- Can be deployed to **Azure App Service**, **AWS EC2**, or **IIS**
- Update connection string for production database
- Configure CORS for production frontend URL
- Set appropriate JWT secret in production environment

### Frontend Deployment
- Can be deployed to **Vercel**, **Netlify**, or **Azure Static Web Apps**
- Update `REACT_APP_API_BASE_URL` to production backend URL
- Run `npm run build` to create optimized production build

---

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

## 📝 License

This project is open source and available for educational purposes.

---

## 📧 Contact

**Repository:** [https://github.com/Rachit77s/TaskifyProject](https://github.com/Rachit77s/TaskifyProject)

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