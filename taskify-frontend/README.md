# 🎯 Taskify Frontend

A modern, responsive task management application built with React. Manage your tasks efficiently with an intuitive interface featuring authentication, filtering, and real-time updates.

![React](https://img.shields.io/badge/React-19.2.0-61DAFB?logo=react)

## ✨ Features

- 🔐 **User Authentication** - Secure login and registration system
- 📝 **Task Management** - Create, read, update, and delete tasks
- 🔍 **Advanced Filtering** - Filter tasks by status, priority, and more
- 📊 **Task Statistics** - Visual dashboard with task counts and stats
- 🎨 **Modern UI** - Clean, gradient-based design with smooth animations
- 📱 **Responsive Design** - Works seamlessly on desktop and mobile devices
- ⚡ **Real-time Updates** - Tasks refresh automatically on changes
- 🚀 **Performance Optimized** - Fast loading and efficient state management

## 🛠️ Tech Stack

- **React 19.2.0** - Modern React with hooks
- **Axios** - HTTP client for API communication
- **React Icons** - Beautiful icon library
- **date-fns** - Date formatting and manipulation
- **Context API** - Global state management for authentication
- **CSS3** - Modern styling with gradients and animations

## 📋 Prerequisites

Before running this project, ensure you have:

- **Node.js** (v14 or higher)
- **npm** (v6 or higher)
- **Backend API** running on `http://localhost:5226`

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone <repository-url>
cd taskify-frontend
```

### 2. Install Dependencies

```bash
npm install
```

### 3. Start the Development Server

```bash
npm start
```

The app will open automatically at [http://localhost:3000](http://localhost:3000)

### 4. Login

Use one of the demo credentials:

**Admin Account:**
- Username: `rachit`
- Password: `Admin@123`

**Test User:**
- Username: `testuser`
- Password: `Test@123`

Or create a new account using the signup form.

## 📁 Project Structure

```
taskify-frontend/
├── public/
│   ├── index.html          # HTML template
│   ├── manifest.json       # PWA manifest
│   └── robots.txt          # SEO robots file
├── src/
│   ├── components/         # Reusable React components
│   │   ├── FilterBar.js    # Task filtering controls
│   │   ├── FilterBar.css
│   │   ├── TaskForm.js     # Create/edit task modal
│   │   ├── TaskForm.css
│   │   ├── TaskList.js     # Task cards grid display
│   │   └── TaskList.css
│   ├── context/            # React Context for state management
│   │   └── AuthContext.js  # Authentication context & logic
│   ├── pages/              # Page components
│   │   ├── Login.js        # Login/signup page
│   │   └── Login.css
│   ├── services/           # API service layer
│   │   └── taskService.js  # Task API calls & interceptors
│   ├── App.js              # Main application component
│   ├── App.css             # Global styles
│   ├── index.js            # React entry point
│   └── index.css           # Base CSS styles
├── .gitignore              # Git ignore rules
├── package.json            # Dependencies and scripts
└── README.md               # This file
```

## 🎨 Design Overview

### Architecture

The application follows a **modular component-based architecture** with clear separation of concerns:

1. **Presentation Layer** (`components/`) - Reusable UI components
2. **Page Layer** (`pages/`) - Full page views
3. **Business Logic** (`services/`) - API communication and data handling
4. **State Management** (`context/`) - Global authentication state

### Key Design Patterns

- **Service Layer Pattern** - API calls abstracted in `taskService.js`
- **Context API Pattern** - Global auth state managed via React Context
- **Component Composition** - Modular, reusable components
- **Controlled Components** - Form inputs managed by React state

### UI/UX Design

- **Color Scheme**: Purple gradient theme (`#667eea` to `#764ba2`)
- **Typography**: Inter font family for modern readability
- **Spacing**: Consistent padding and margins (multiples of 4px)
- **Interactive Elements**: Hover effects, smooth transitions, shadows
- **Accessibility**: Semantic HTML, proper labels, keyboard navigation

### State Flow

```
User Action → Component → Service Layer → API → Response
                ↓                                    ↓
            Local State ← Context (Auth) ← Update State
```

### Authentication Flow

```
1. User enters credentials → Login.js
2. AuthContext.login() → API call
3. Token received → localStorage
4. App.js detects user → Shows task manager
5. All API calls include token → Axios interceptor
```

## 📦 Available Scripts

### `npm start`
Runs the app in development mode at [http://localhost:3000](http://localhost:3000)

### `npm test`
Launches the test runner in interactive watch mode

### `npm run build`
Builds the app for production to the `build` folder

### `npm run eject`
**Note: This is a one-way operation!**
Ejects from Create React App for full configuration control

## 🔌 API Integration

The frontend connects to a .NET backend API at `http://localhost:5226/api`

### Main Endpoints Used:
- `POST /Auth/register` - User registration
- `POST /Auth/login` - User authentication
- `GET /Tasks` - Fetch all tasks
- `POST /Tasks` - Create new task
- `PUT /Tasks/{id}` - Update task
- `DELETE /Tasks/{id}` - Delete task
- `GET /Tasks/filter` - Filter tasks by status/priority

## 🔒 Security Features

- JWT token-based authentication
- Axios interceptors for automatic token injection
- Protected routes (task manager only shown when authenticated)
- Graceful error handling for 401 unauthorized errors
- Password minimum length validation (6 characters)

## 🐛 Troubleshooting

### App won't start
```bash
# Clear node modules and reinstall
rm -rf node_modules package-lock.json
npm install
npm start
```

### Tasks not loading
- Ensure backend API is running on `http://localhost:5226`
- Check browser console for error messages
- Verify you're logged in with valid credentials

### Infinite reload issue
- Clear browser localStorage: `localStorage.clear()`
- Refresh the page and login again

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License.

## 👨‍💻 Author

Created with ❤️ for efficient task management

---

**Happy Task Managing! 🚀**
