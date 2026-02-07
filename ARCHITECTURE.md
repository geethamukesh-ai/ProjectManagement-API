# PIMS Architecture

## System Architecture Diagram

```
┌─────────────────────────────────────────────────────────────────────┐
│                         PIMS ARCHITECTURE                            │
└─────────────────────────────────────────────────────────────────────┘

┌──────────────────────────────────────────────────────────────────────┐
│                           FRONTEND (React 18)                         │
├──────────────────────────────────────────────────────────────────────┤
│                                                                       │
│  ┌─────────────┐  ┌──────────────┐  ┌─────────────┐                │
│  │   Login     │  │   Register   │  │  Dashboard  │                 │
│  └─────────────┘  └──────────────┘  └─────────────┘                 │
│                                                                       │
│  ┌─────────────┐  ┌──────────────┐  ┌─────────────┐                │
│  │  Projects   │  │    Users     │  │  Contracts  │                 │
│  └─────────────┘  └──────────────┘  └─────────────┘                 │
│                                                                       │
│  ┌─────────────┐  ┌──────────────────────────────────┐              │
│  │  Documents  │  │     Navigation & Routing         │              │
│  └─────────────┘  └──────────────────────────────────┘              │
│                                                                       │
│  ┌──────────────────────────────────────────────────────────────┐   │
│  │              API Service Layer (axios)                       │   │
│  │  • Authentication  • Projects  • Users                       │   │
│  │  • Contracts       • Documents • Approvals                   │   │
│  └──────────────────────────────────────────────────────────────┘   │
│                                                                       │
└───────────────────────────┬───────────────────────────────────────────┘
                            │
                            │ HTTP/HTTPS (JWT Token)
                            │
┌───────────────────────────▼───────────────────────────────────────────┐
│                    ASP.NET CORE WEB API (.NET 8)                      │
├──────────────────────────────────────────────────────────────────────┤
│                                                                       │
│  ┌─────────────────────────  CONTROLLERS  ─────────────────────┐    │
│  │                                                               │    │
│  │  AuthController      │  ProjectsController                   │    │
│  │  UsersController     │  ProjectMembersController             │    │
│  │  ContractsController │  DocumentsController                  │    │
│  │  ApprovalsController                                         │    │
│  └───────────────────────────────┬───────────────────────────────┘   │
│                                  │                                    │
│  ┌───────────────────────────────▼───────────────────────────────┐   │
│  │                        SERVICES LAYER                         │   │
│  │  • AuthService     (JWT generation, validation)               │   │
│  │  • UserService     (User management)                          │   │
│  │  • ProjectService  (Project CRUD operations)                  │   │
│  └───────────────────────────────┬───────────────────────────────┘   │
│                                  │                                    │
│  ┌───────────────────────────────▼───────────────────────────────┐   │
│  │                      DATA ACCESS LAYER                        │   │
│  │              ApplicationDbContext (EF Core)                   │   │
│  └───────────────────────────────┬───────────────────────────────┘   │
│                                  │                                    │
└──────────────────────────────────┼────────────────────────────────────┘
                                   │
                                   │ Entity Framework Core
                                   │
┌──────────────────────────────────▼────────────────────────────────────┐
│                         SQL SERVER DATABASE                           │
├──────────────────────────────────────────────────────────────────────┤
│                                                                       │
│  ┌──────────┐  ┌──────────┐  ┌────────────────┐  ┌──────────┐      │
│  │  Users   │  │ Projects │  │ ProjectMembers │  │Contracts │      │
│  └──────────┘  └──────────┘  └────────────────┘  └──────────┘      │
│                                                                       │
│  ┌──────────┐  ┌──────────┐                                         │
│  │Documents │  │Approvals │                                         │
│  └──────────┘  └──────────┘                                         │
│                                                                       │
└──────────────────────────────────────────────────────────────────────┘
```

## Request Flow

### Authentication Flow
```
1. User → Login Page (React)
2. Submit Credentials → POST /api/auth/login
3. AuthService → Validate credentials (BCrypt)
4. Generate JWT Token
5. Return Token + User Info
6. Store in localStorage
7. Redirect to Dashboard
```

### Data Retrieval Flow
```
1. User → Projects Page (React)
2. GET /api/projects (with JWT in header)
3. Middleware → Validate JWT Token
4. ProjectsController → Authorize user
5. ProjectService → Business logic
6. EF Core → Query database
7. Return data as JSON
8. React → Display in UI
```

### Create Project Flow
```
1. User fills form → Projects Page
2. POST /api/projects (with data + JWT)
3. Middleware → Validate JWT
4. Controller → Authorize (Admin/Manager only)
5. Service → Validate data
6. Service → Set timestamps
7. EF Core → Insert to database
8. Return created project
9. React → Update UI
```

## Technology Stack

### Frontend
- **Framework**: React 18
- **Routing**: React Router v6
- **HTTP Client**: Axios
- **Styling**: Custom CSS
- **State**: React Hooks

### Backend
- **Framework**: ASP.NET Core 8.0
- **ORM**: Entity Framework Core 8.0
- **Database**: SQL Server
- **Authentication**: JWT Bearer
- **Password**: BCrypt
- **API Docs**: Swagger/OpenAPI

## Security Layers

```
┌──────────────────────────────────────┐
│  1. HTTPS/TLS Encryption             │
├──────────────────────────────────────┤
│  2. JWT Token Validation             │
├──────────────────────────────────────┤
│  3. Role-Based Authorization         │
├──────────────────────────────────────┤
│  4. Input Validation                 │
├──────────────────────────────────────┤
│  5. BCrypt Password Hashing          │
├──────────────────────────────────────┤
│  6. CORS Policy                      │
└──────────────────────────────────────┘
```

## Data Model Relationships

```
User ──────< ProjectMember >────── Project
                                      │
                                      ├────< Contract
                                      │
                                      └────< Document
                                              │
                                              └────< Approval
```

## Deployment Architecture

```
┌─────────────────────┐
│   React Build       │
│   (Static Files)    │
└──────────┬──────────┘
           │
           │ Deployed to
           ▼
┌─────────────────────┐         ┌─────────────────────┐
│   Web Server        │         │   API Server        │
│   (IIS/Nginx)       │◄────────┤   (IIS/Kestrel)     │
│   Port 80/443       │         │   Port 5000         │
└─────────────────────┘         └──────────┬──────────┘
                                           │
                                           │
                                           ▼
                                ┌─────────────────────┐
                                │   SQL Server        │
                                │   Database          │
                                └─────────────────────┘
```

