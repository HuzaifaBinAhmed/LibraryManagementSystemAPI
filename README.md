Library Management System - Backend (ASP.NET Core 9)

A simple Library Management System backend built with ASP.NET Core 9, Entity Framework Core, and JWT authentication. Designed as an internship task to demonstrate role-based authorization, CRUD operations, and basic validations.

Features
Authentication & Authorization

Register/Login for both Librarians and Students.

Roles:

librarian: Full CRUD access on books and can view all borrow records.

student: Can borrow, return, and view books.

JWT tokens for secure authentication.

Middleware for role-based authorization.

Entities

Users

Id (PK)

Name

Email (unique)

PasswordHash

Role (librarian or student)

CreatedAt, UpdatedAt

Books

Id (PK)

Title, Author, ISBN (unique), Category

Quantity (available copies)

CreatedAt, UpdatedAt

BorrowRecords

Id (PK)

UserId (FK → Users.Id)

BookId (FK → Books.Id)

BorrowDate, ReturnDate, DueDate

Status (borrowed / returned)

API Endpoints
Auth

POST /api/auth/register – Register a new user.

POST /api/auth/login – Login and receive a JWT token.

User

GET /api/users/me – Get current user details.

Books

POST /api/books – Add a new book (Librarian only).

GET /api/books – List all books (with optional search/filter/pagination).

GET /api/books/{id} – Get book details.

PUT /api/books/{id} – Update book details (Librarian only).

DELETE /api/books/{id} – Delete a book (Librarian only).

Borrow

POST /api/borrow/{bookId} – Borrow a book (Student only).

PUT /api/borrow/return/{id} – Return a book (Student only).

GET /api/borrow/my – View student's borrow history.

GET /api/borrow/all – View all borrow records (Librarian only).

Validation & Logic

Email format and password length validated during registration.

Book quantity cannot be negative.

Prevent borrowing if no quantity is available.

Borrow limit: maximum 3 books per student.

Borrowing:

Checks availability (quantity > 0)

Reduces book quantity by 1

Sets DueDate to 7 days later

Returning:

Increases book quantity by 1

Updates status to returned

Technologies

ASP.NET Core 9

Entity Framework Core 8 (SQL Server)

JWT Authentication

BCrypt for password hashing

Swagger for API testing
