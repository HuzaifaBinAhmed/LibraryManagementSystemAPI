A **Library Management System backend** built with **ASP.NET Core 9** and **Entity Framework Core 8**, implementing **JWT authentication**, **role-based authorization**, and basic CRUD operations. Designed as an **internship project** to demonstrate backend development skills.

---

## Features

### **Authentication & Authorization**
- Register/Login for both **Librarians** and **Students**
- Roles:
  - `librarian`: Full CRUD on books & can view all borrow records
  - `student`: Borrow, return, and view books
- JWT tokens for authentication
- Middleware for role-based authorization

### **Entities**
- **Users**: Id, Name, Email, PasswordHash, Role, CreatedAt, UpdatedAt
- **Books**: Id, Title, Author, ISBN, Category, Quantity, CreatedAt, UpdatedAt
- **BorrowRecords**: Id, UserId, BookId, BorrowDate, ReturnDate, DueDate, Status (`borrowed` / `returned`)

### **Validations & Logic**
- Email format and password length validated
- Book quantity ≥ 0
- Prevent borrowing if no quantity available
- Borrow limit: 3 books per student
- Borrowing: reduce quantity by 1, set DueDate = 7 days
- Returning: increase quantity by 1, mark status as `returned`

---

## 📦 API Endpoints

### Auth
| Method | Endpoint | Role | Description |
|--------|---------|------|-------------|
| POST | `/api/auth/register` | Any | Register new user |
| POST | `/api/auth/login` | Any | Login and receive JWT token |

### Users
| Method | Endpoint | Role | Description |
|--------|---------|------|-------------|
| GET | `/api/users/me` | Any logged-in | Get current user details |

### Books
| Method | Endpoint | Role | Description |
|--------|---------|------|-------------|
| POST | `/api/books` | Librarian | Add a new book |
| GET | `/api/books` | Any | List all books |
| GET | `/api/books/{id}` | Any | Get book details |
| PUT | `/api/books/{id}` | Librarian | Update book |
| DELETE | `/api/books/{id}` | Librarian | Delete book |

### Borrow
| Method | Endpoint | Role | Description |
|--------|---------|------|-------------|
| POST | `/api/borrow/{bookId}` | Student | Borrow a book |
| PUT | `/api/borrow/return/{id}` | Student | Return a book |
| GET | `/api/borrow/my` | Student | View student's borrow history |
| GET | `/api/borrow/all` | Librarian | View all borrow records |

---

## ⚙️ Technologies
- ASP.NET Core 9
- Entity Framework Core 8 (SQL Server)
- JWT Authentication
- BCrypt for password hashing
- Swagger for API testing

---


LibraryManagementSystem/
├─ Controllers/
│  ├─ AuthController.cs
│  ├─ BooksController.cs
│  ├─ BorrowController.cs
│  └─ UsersController.cs
├─ Models/
│  ├─ User.cs
│  ├─ Book.cs
│  └─ BorrowRecord.cs
├─ Data/
│  └─ AppDbContext.cs
├─ Services/
│  └─ JwtTokenService.cs
├─ DTOs/
│  ├─ RegisterDto.cs
│  ├─ LoginDto.cs
│  └─ AuthResponseDto.cs
├─ appsettings.json
├─ Program.cs
└─ README.md

⚖️ License

This project is under MIT License.

