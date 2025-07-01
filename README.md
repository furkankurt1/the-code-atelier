# the-code-atelier
Repository for interview candidate submissions

## Submission Instructions

Please follow these steps to create your personal branch, work on your solution, and submit your code for review.

1. Clone the repository

   git clone https://github.com/boligmappa/the-code-atelier
   cd the-code-atelier

2. Create a new branch from main

   Replace "your-branch-name" with your full name or assigned candidate ID.

   git checkout -b your-branch-name main

3. Make your changes

   After editing or adding files, stage and commit your work.

   git add .
   git commit -m "Initial submission by Your Name"

4. Push your branch to GitHub

   git push origin your-branch-name

5. (Optional) Open a Pull Request

   After pushing your branch, you can open a Pull Request on GitHub to merge it into main for review.

## Notes

🛠 Mini Task Management API
This project is a demo application built for an interview assignment. Due to a limited timeframe of around 4 hours, I couldn't implement all features fully. Some repetitive functionalities were skipped. However, I believe the project is still sufficient to demonstrate my development approach and architectural mindset.

If you're interested, I’d be happy to showcase a real-world project I’ve developed called "Mealar", which involves more advanced technologies and a broader scope.

🔧 Technologies & Architecture
.NET 8

SQLite with Entity Framework Core (with migrations)

Clean Architecture

NTier Layered Architecture

CQRS + MediatR

Repository Pattern

SOLID Principles

Minimal API + Swagger UI

🚫 Missing Features
Due to time constraints, the following areas are not yet implemented:

✅ Unit Tests

✅ Exception Handling & Logging

✅ AOP / Cross-Cutting Concerns (e.g., Authorization, Caching)

🚀 How to Run
Run the project (dotnet run or via IDE).

Create a user via /api/user/create endpoint.

Use "Admin" or "User" as the role.

Log in via /api/auth/login.

Copy the JWT token and authorize via Swagger.

Create a project using /api/project/create.

Create another user if needed.

Log in again with the new user and get a new JWT.

Use /api/task/create to create tasks.

💬 Additional Notes
This project aims to showcase my architectural and coding approach. I’d be happy to explain my reasoning behind the implementation and discuss how it can be improved further.

📫 Contact
I can also demonstrate the Mealar project or share code from other production-level applications if needed.


