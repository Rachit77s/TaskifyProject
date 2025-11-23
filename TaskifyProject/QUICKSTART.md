# Taskify - Quick Start Guide

Get up and running in 5 minutes!

---

## **Fast Setup**

### **1. Backend (Terminal 1)**

```bash
cd TaskifyProject
dotnet run
```

? Backend running on: `http://localhost:5226`

### **2. Frontend (Terminal 2)**

```bash
cd taskify-frontend
npm install    # First time only
npm start
```

? Frontend running on: `http://localhost:3000`

---

## **Login**

**Browser opens automatically to:** `http://localhost:3000/login`

**Use these credentials:**
```
Username: rachit
Password: Admin@123
```

**Or:**
```
Username: testuser
Password: Test@123
```

---

## **Test the App**

### **After Login:**

1. View tasks (Rachit has 6, TestUser has 4)
2. Create new task (click "+ Add New Task")
3. Edit task (click edit icon)
4. Toggle status (click checkbox)
5. Delete task (click delete icon)
6. Filter tasks (use filter bar)
7. Logout (click Logout button)

---

## **API Testing**

### **Swagger UI:** `http://localhost:5226/`

**Quick Test:**
1. Click `POST /api/auth/login`
2. Try it out
3. Enter: `{ "usernameOrEmail": "rachit", "password": "Admin@123" }`
4. Execute
5. Copy the `token` from response
6. Click "Authorize" (?? icon)
7. Enter: `Bearer <your-token>`
8. Now test any endpoint!

---

## **Reset Database**

```bash
cd TaskifyProject
dotnet ef database drop --force
dotnet ef database update
dotnet run
```

Database auto-seeds with 3 users and 14 tasks.

---