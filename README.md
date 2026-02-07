# PIMS - Project Information Management System

A comprehensive full-stack application for managing projects, users, contracts, documents, and approvals.

## 🚀 Features

### Backend API (ASP.NET Core)
- **Authentication & Authorization**: JWT-based authentication with role-based access control
- **Project Management**: Full CRUD operations for projects with detailed tracking
- **User Management**: User registration, authentication, and profile management
- **Contract Management**: Track and manage project contracts with approval workflows
- **Document Management**: Upload, version, and manage project documents
- **Approval System**: Workflow for document approvals
- **RESTful API**: Clean, well-documented API endpoints
- **Swagger/OpenAPI**: Interactive API documentation

### Frontend UI (React)
- **Modern React Application**: Built with React 18 and React Router
- **Authentication UI**: Login and registration pages
- **Dashboard**: Overview of all system modules
- **Project Management**: Create, view, and manage projects
- **User Management**: View system users and their roles
- **Contract Tracking**: Monitor contracts and their status
- **Document Management**: Browse and manage project documents
- **Responsive Design**: Works on desktop and mobile devices

## 📋 Prerequisites

### For API
- .NET 8.0 SDK or later
- SQL Server (LocalDB or full installation)
- Visual Studio 2022 or VS Code (recommended)

### For UI
- Node.js 18+ and npm
- Modern web browser

## 🛠️ Installation & Setup

### 1. API Setup

```bash
cd ProjectManagement-API

# Restore NuGet packages
dotnet restore

# Update database connection string in appsettings.json if needed
# Default: Server=(localdb)\\mssqllocaldb;Database=ProjectManagementDB;...

# Create and migrate database
dotnet ef migrations add InitialCreate
dotnet ef database update

# Run the API
dotnet run
```

The API will be available at `https://localhost:5001` (HTTPS) or `http://localhost:5000` (HTTP)

Swagger UI will be available at: `https://localhost:5001/swagger`

### 2. UI Setup

```bash
cd UI

# Install dependencies
npm install

# Configure API URL (optional)
# Create a .env file and set:
# REACT_APP_API_URL=http://localhost:5000/api

# Start the development server
npm start
```

The UI will be available at `http://localhost:3000`

## 📚 API Documentation

### Authentication Endpoints

#### Register a new user
```http
POST /api/auth/register
Content-Type: application/json

{
  "username": "johndoe",
  "password": "SecurePass123!",
  "fullName": "John Doe",
  "email": "john@example.com",
  "role": "User"
}
```

#### Login
```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "johndoe",
  "password": "SecurePass123!"
}
```

Response:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIs...",
  "username": "johndoe",
  "fullName": "John Doe",
  "role": "User",
  "expiresAt": "2024-02-07T12:00:00Z"
}
```

### Project Endpoints

- `GET /api/projects` - Get all projects
- `GET /api/projects/{id}` - Get project by ID
- `POST /api/projects` - Create new project (Admin/Manager)
- `PUT /api/projects/{id}` - Update project (Admin/Manager)
- `DELETE /api/projects/{id}` - Delete project (Admin)

### User Endpoints

- `GET /api/users` - Get all users
- `GET /api/users/{id}` - Get user by ID
- `POST /api/users` - Create user (Admin)
- `PUT /api/users/{id}` - Update user (Admin)
- `DELETE /api/users/{id}` - Delete user (Admin)

### Contract Endpoints

- `GET /api/contracts` - Get all contracts
- `GET /api/contracts/{id}` - Get contract by ID
- `GET /api/contracts/project/{projectId}` - Get contracts for a project
- `POST /api/contracts` - Create contract (Admin/Manager)
- `PUT /api/contracts/{id}` - Update contract (Admin/Manager)
- `DELETE /api/contracts/{id}` - Delete contract (Admin)

### Document Endpoints

- `GET /api/documents` - Get all documents
- `GET /api/documents/{id}` - Get document by ID
- `GET /api/documents/project/{projectId}` - Get documents for a project
- `POST /api/documents` - Upload document
- `PUT /api/documents/{id}` - Update document (Admin/Manager)
- `DELETE /api/documents/{id}` - Delete document (Admin/Manager)

### Approval Endpoints

- `GET /api/approvals` - Get all approvals
- `GET /api/approvals/{id}` - Get approval by ID
- `GET /api/approvals/document/{documentId}` - Get approvals for a document
- `POST /api/approvals` - Create approval (Admin/Manager)
- `PUT /api/approvals/{id}` - Update approval (Admin/Manager)

## 🔐 Security

- **JWT Authentication**: Secure token-based authentication
- **Role-Based Access**: Three user roles (Admin, Manager, User)
  - New users are automatically assigned "User" role
  - Only Admins can promote users to Manager or Admin roles
- **Password Hashing**: BCrypt for secure password storage
- **HTTPS Support**: SSL/TLS encryption
- **CORS Configuration**: Configurable cross-origin resource sharing
- **Secret Key Management**: JWT secret key must be configured securely (see Configuration section)

## 🏗️ Architecture

### Backend Structure
```
ProjectManagementAPI/
├── Controllers/          # API endpoints
├── Services/            # Business logic
├── Models/              # Data models
├── Data/                # Database context
├── DTOs/                # Data transfer objects
├── Program.cs           # App configuration
└── appsettings.json     # Configuration
```

### Frontend Structure
```
UI/
├── public/              # Static files
├── src/
│   ├── components/      # Reusable components
│   ├── pages/           # Page components
│   ├── services/        # API service layer
│   ├── App.js           # Main app component
│   └── index.js         # Entry point
└── package.json         # Dependencies
```

## 📊 Database Schema

- **Users**: User accounts and authentication
- **Projects**: Project information and tracking
- **ProjectMembers**: Team members assigned to projects
- **Contracts**: Project contracts and agreements
- **Documents**: Project documents and files
- **Approvals**: Document approval workflow

## 🎨 UI Features

- **Modern Design**: Clean, professional interface
- **Responsive Layout**: Works on all device sizes
- **Real-time Updates**: Dynamic data loading
- **Form Validation**: Client-side validation
- **Error Handling**: User-friendly error messages
- **Loading States**: Visual feedback during operations

## 🔧 Configuration

### API Configuration (appsettings.json)

⚠️ **IMPORTANT SECURITY NOTE**: Before running in production, you MUST change the JWT SecretKey!

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your SQL Server connection string"
  },
  "JwtSettings": {
    "SecretKey": "GENERATE_A_STRONG_RANDOM_KEY_HERE",
    "Issuer": "ProjectManagementAPI",
    "Audience": "ProjectManagementAPIUsers",
    "ExpirationMinutes": 60
  }
}
```

**To generate a secure secret key:**
```bash
# Using PowerShell
[Convert]::ToBase64String((1..64 | ForEach-Object { Get-Random -Maximum 256 }))

# Using OpenSSL
openssl rand -base64 64

# Using Node.js
node -e "console.log(require('crypto').randomBytes(64).toString('base64'))"
```

**Best Practices:**
- Never commit the production secret key to source control
- Use environment variables or Azure Key Vault for production
- Rotate keys periodically
- Keep the minimum key length of 32 characters

### UI Configuration (.env)
```
REACT_APP_API_URL=http://localhost:5000/api
```

## 🚀 Deployment

### API Deployment
1. Publish the application: `dotnet publish -c Release`
2. Deploy to IIS, Azure App Service, or Docker
3. Update connection string for production database
4. Configure HTTPS certificate

### UI Deployment
1. Build the application: `npm run build`
2. Deploy the `build/` folder to a web server
3. Configure environment variables for production API URL

## 📝 Default Credentials

After running migrations, you can create an admin user through the registration endpoint with role "Admin".

## 🧪 Testing

### API Testing
- Use Swagger UI at `/swagger` for interactive testing
- Use Postman or similar tools for automated testing

### UI Testing
- Manual testing through the web interface
- Unit tests: `npm test` (in UI directory)

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

## 📄 License

This project is licensed under the MIT License.

## 👥 User Roles

- **Admin**: Full system access, can manage users and delete resources
- **Manager**: Can create and manage projects, contracts, and documents
- **User**: Can view projects and upload documents

## 🆘 Support

For issues or questions, please create an issue in the repository.

## 🔄 Future Enhancements

- File upload functionality for documents
- Advanced search and filtering
- Project analytics and reporting
- Email notifications
- Project timeline visualization
- Export functionality (PDF, Excel)
- Audit logging
- Two-factor authentication

---

**Built with ❤️ using ASP.NET Core and React**
