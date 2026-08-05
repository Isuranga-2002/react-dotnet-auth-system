# Full-Stack Authentication System

A lightweight, full-stack web application demonstrating a secure authentication flow. This project implements user registration and login functionalities using a modern tech stack, serving as a foundation for building secure web applications.

## Features
* **User Registration:** Secure sign-up flow with password hashing.
* **User Authentication:** Sign-in functionality utilizing JSON Web Tokens (JWT).
* **RESTful API:** Backend endpoints built with .NET Web API.
* **Relational Database:** Data persistence managed via SQL Server and Entity Framework Core.
* **Modern UI:** Component-driven frontend built with React.

## Tech Stack
**Frontend:**
* React
* React Router (for navigation)

**Backend:**
* .NET Web API (C#)
* Entity Framework (EF) Core

**Database:**
* Microsoft SQL Server

## Local Development Setup

### Prerequisites
* [Node.js](https://nodejs.org/) installed
* [.NET SDK](https://dotnet.microsoft.com/download) installed
* SQL Server and SQL Server Management Studio (SSMS) or Azure Data Studio

### Backend Setup
1. Navigate to the `backend` directory.
2. Update the SQL Server connection string in `appsettings.json`.
3. Open a terminal and run the database migrations:
   ```bash
   dotnet ef database update