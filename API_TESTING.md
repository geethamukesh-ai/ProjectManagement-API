# API Testing Guide

## Using Swagger UI (Recommended for Beginners)

1. Start the API: `dotnet run`
2. Open browser: `http://localhost:5000/swagger`
3. All endpoints are listed with try-it-now functionality

## Manual API Testing Examples

### 1. Register a New User

```bash
curl -X POST "http://localhost:5000/api/auth/register" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "testuser",
    "password": "Test123!",
    "fullName": "Test User",
    "email": "test@example.com",
    "role": "User"
  }'
```

Response:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIs...",
  "username": "testuser",
  "fullName": "Test User",
  "role": "User",
  "expiresAt": "2024-02-07T12:00:00Z"
}
```

### 2. Login

```bash
curl -X POST "http://localhost:5000/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "testuser",
    "password": "Test123!"
  }'
```

**Save the token from the response!**

### 3. Get All Projects (Requires Authentication)

```bash
# Replace YOUR_TOKEN with the token from login
curl -X GET "http://localhost:5000/api/projects" \
  -H "Authorization: Bearer YOUR_TOKEN"
```

### 4. Create a New Project

```bash
curl -X POST "http://localhost:5000/api/projects" \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "projectCode": "PROJ001",
    "projectName": "Sample Project",
    "clientName": "ABC Corp",
    "status": "Planning",
    "description": "This is a test project",
    "startDate": "2024-01-01T00:00:00",
    "endDate": "2024-12-31T00:00:00",
    "budget": 100000
  }'
```

### 5. Get All Users

```bash
curl -X GET "http://localhost:5000/api/users" \
  -H "Authorization: Bearer YOUR_TOKEN"
```

### 6. Create a Contract

```bash
curl -X POST "http://localhost:5000/api/contracts" \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "projectId": 1,
    "contractNumber": "CNT-2024-001",
    "contractType": "Service Agreement",
    "vendor": "Tech Solutions Inc",
    "amount": 50000,
    "startDate": "2024-01-01T00:00:00",
    "endDate": "2024-06-30T00:00:00",
    "status": "Active",
    "approvalStatus": "Approved",
    "terms": "Standard terms and conditions"
  }'
```

### 7. Upload a Document

```bash
curl -X POST "http://localhost:5000/api/documents" \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "projectId": 1,
    "fileName": "project-plan.pdf",
    "filePath": "/documents/project-plan.pdf",
    "fileSize": 1024000,
    "uploadedBy": "testuser",
    "status": "Active",
    "version": 1,
    "description": "Initial project plan"
  }'
```

### 8. Get Documents for a Project

```bash
curl -X GET "http://localhost:5000/api/documents/project/1" \
  -H "Authorization: Bearer YOUR_TOKEN"
```

### 9. Create an Approval

```bash
curl -X POST "http://localhost:5000/api/approvals" \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "documentId": 1,
    "status": "Approved",
    "approvedBy": "admin",
    "approvalDate": "2024-02-07T00:00:00",
    "comments": "Document looks good"
  }'
```

## Common HTTP Status Codes

- **200 OK**: Request successful
- **201 Created**: Resource created successfully
- **204 No Content**: Successful deletion
- **400 Bad Request**: Invalid input data
- **401 Unauthorized**: Missing or invalid token
- **403 Forbidden**: Insufficient permissions
- **404 Not Found**: Resource not found
- **409 Conflict**: Resource already exists

## Testing with Postman

1. Import the API endpoints from Swagger
2. Create an environment with:
   - `base_url`: `http://localhost:5000/api`
   - `token`: (will be set after login)
3. Create a login request and save token to environment
4. Use `{{token}}` in Authorization header for other requests

## Testing Workflow

1. **Register** a new user (Admin role recommended)
2. **Login** to get authentication token
3. **Create** a project
4. **Add** contracts to the project
5. **Upload** documents
6. **Create** approvals for documents
7. **View** all data through GET endpoints

## Role-Based Testing

### As Admin
- Can perform all operations
- Create/edit/delete projects, users, contracts, documents

### As Manager
- Can create/edit projects and contracts
- Cannot delete users or projects

### As User
- Can view projects
- Can upload documents
- Cannot create or edit projects

## Security Testing

### Test Invalid Token
```bash
curl -X GET "http://localhost:5000/api/projects" \
  -H "Authorization: Bearer invalid_token"
# Should return 401 Unauthorized
```

### Test Expired Token
- Wait for token expiration (default 60 minutes)
- Try to use the token
- Should return 401 Unauthorized

### Test Role Permissions
- Login as User role
- Try to delete a project
- Should return 403 Forbidden

## Performance Testing

Use tools like Apache Bench or k6 for load testing:

```bash
# Example with Apache Bench
ab -n 1000 -c 10 -H "Authorization: Bearer YOUR_TOKEN" \
  http://localhost:5000/api/projects
```

---

**Happy Testing! 🧪**
