# UserContactApi
# Contact CRUD API

## Overview

This project is a .NET 8 API application that provides CRUD (Create, Read, Update, Delete) operations for managing contacts.
The API is built using .NET minimal APIs and uses dependency injection for service and repository layers. 
It includes unit tests with an in-memory database for testing purposes.

## Technologies Used

- .NET 8
- ASP.NET Core Minimal APIs
- Entity Framework Core (In-Memory Database for testing)
- Dependency Injection
- Xunit for Unit Testing


## Project Structure

- **Endpoints/ContactEndPoints.cs**: Defines minimal API endpoints for contact CRUD operations.
- **Services/IUserContactService.cs**: Service interface for contact operations.
- **Services/UserContactService.cs**: Implementation of the contact service.
- **Repositories/IUserContactRepository.cs**: Repository interface for contact data operations.
- **Repositories/UserContactRepository.cs**: Implementation of the contact repository.
- **Models/Contact.cs**: Defines the Contact entity.
- **Data/AppDbContext.cs**: EF Core DbContext for the application.
- **Tests/UserContactsApi.Tests/**: Contains unit tests for the API using an in-memory database.

## Endpoints

### Create a Contact

**POST** `/contacts`

**Request Body:**
```json
{ firstName = "John", lastName = "Doe", email = "john@example.com", phone = "9999997890"}


### Explanation of CORS Configuration Section

- **`AllowAnyOrigin`**: Allows requests from any origin. Useful for development.

