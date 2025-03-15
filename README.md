# Learning System

## Description
Learning System is a web-based application designed for managing courses, students, and educational content. This project provides functionalities for user authentication, course enrollment, and content management, making it a valuable tool for learning platforms.

## Technologies Used
- **Frontend:** HTML, CSS, JavaScript
- **Backend:** C#, ASP.NET Core
- **Database:** MS SQL Server, Entity Framework Core
- **Authentication:** ASP.NET Identity
- **Additional Tools:**
  - AutoMapper (for object mapping)
  - Dependency Injection (for service management)
  - Unit Testing with xUnit

## Features
- User authentication (registration, login, logout)
- Role-based access control (students, teachers, administrators)
- Course creation, editing, and deletion
- Course enrollment for students
- Content management (lessons, assignments, quizzes)
- Database integration with Entity Framework Core

## Installation & Setup
### Prerequisites
- .NET 6 or later
- MS SQL Server
- Node.js (if frontend dependencies are managed)

### Steps to Run the Project
1. Clone the repository:
   ```sh
   git clone <repository-url>
   cd Learning-System
   ```
2. Configure the database connection in `appsettings.json`.
3. Apply database migrations:
   ```sh
   dotnet ef database update
   ```
4. Run the application:
   ```sh
   dotnet run
   ```

## Project Structure
```
Learning-System/
│── LearningSystem.Web/ (Frontend & API controllers)
│── LearningSystem.Data/ (Database models and EF Core configurations)
│── LearningSystem.Services/ (Business logic and service layer)
│── LearningSystem.Tests/ (Unit and integration tests)
```

## License
This project is open-source and licensed under the MIT License.


