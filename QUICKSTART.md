# Quick Start Guide - PIMS

## 🚀 Get Started in 5 Minutes

### Prerequisites Check
- ✅ .NET 8.0 SDK installed? Run: `dotnet --version`
- ✅ Node.js 18+ installed? Run: `node --version`
- ✅ SQL Server available? (LocalDB works fine)

### Step 1: Start the API (Terminal 1)

```bash
# Navigate to project root
cd ProjectManagement-API

# Restore packages
dotnet restore

# Create database (if using migrations)
dotnet ef migrations add InitialCreate
dotnet ef database update

# Run the API
dotnet run
```

✅ API should be running at: `http://localhost:5000`
📚 Swagger UI available at: `http://localhost:5000/swagger`

### Step 2: Start the UI (Terminal 2)

```bash
# Navigate to UI folder
cd ProjectManagement-API/UI

# Install dependencies (first time only)
npm install

# Start development server
npm start
```

✅ UI should open automatically at: `http://localhost:3000`

### Step 3: Create Your First User

1. Open the UI at `http://localhost:3000`
2. Click "Register here"
3. Fill in the form:
   - Username: `admin`
   - Full Name: `Admin User`
   - Email: `admin@pims.com`
   - Password: `Admin123!`
   - Role: `Admin`
4. Click "Register"

### Step 4: Start Managing Projects

You're now logged in! You can:
- ✨ Create new projects
- 👥 View users
- 📝 Manage contracts
- 📁 Handle documents

## 🎯 What to Try First

### Create Your First Project
1. Click on "Projects" in the navbar
2. Click "New Project"
3. Fill in the project details
4. Click "Create Project"

### Explore the API
1. Open Swagger UI: `http://localhost:5000/swagger`
2. Try the `/api/auth/login` endpoint
3. Use the token to test other endpoints

## 📱 Main Features

| Feature | URL | Description |
|---------|-----|-------------|
| Dashboard | `/` | Overview of all modules |
| Projects | `/projects` | Manage projects |
| Users | `/users` | View all users |
| Contracts | `/contracts` | Track contracts |
| Documents | `/documents` | Document management |

## 🔐 User Roles

- **Admin**: Full access to all features
- **Manager**: Can create/edit projects and contracts
- **User**: Can view and upload documents

## 🆘 Troubleshooting

### API won't start?
- Check if port 5000 is available
- Verify SQL Server is running
- Check connection string in `appsettings.json`

### UI won't start?
- Check if port 3000 is available
- Delete `node_modules` and run `npm install` again
- Verify API URL in environment settings

### Can't login?
- Make sure the API is running
- Check browser console for errors
- Verify the API URL is correct (`http://localhost:5000/api`)

## 📖 Next Steps

- Read the full [README.md](README.md) for detailed documentation
- Explore the Swagger UI for API details
- Check out the code structure
- Customize for your needs!

---

**Happy Project Managing! 🎉**
