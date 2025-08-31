using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BorrowController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BorrowController(AppDbContext context)
        {
            _context = context;
        }

        // Student only → Borrow a book
        [HttpPost("{bookId}")]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> BorrowBook(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null || book.Quantity <= 0)
                return BadRequest(new { message = "Book not available" });

            // ✅ Get email from token instead of int userId
            var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier)
                            ?? User.FindFirstValue(ClaimTypes.Email)
                            ?? User.FindFirstValue(ClaimTypes.Name);

            if (string.IsNullOrEmpty(userEmail))
                return Unauthorized(new { message = "Invalid token: no email found" });

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            if (user == null)
                return Unauthorized(new { message = "User not found" });

            // Check borrow limit
            var activeBorrows = await _context.BorrowRecords
                .CountAsync(r => r.UserId == user.Id && r.Status == "borrowed");
            if (activeBorrows >= 3)
                return BadRequest(new { message = "Borrow limit reached (3 books max)" });

            var record = new BorrowRecord
            {
                UserId = user.Id,
                BookId = bookId,
                BorrowDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(7),
                Status = "borrowed"
            };

            book.Quantity -= 1;
            _context.BorrowRecords.Add(record);
            await _context.SaveChangesAsync();

            return Ok(record);
        }

        // Student only → Return book
        [HttpPut("return/{id}")]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            var record = await _context.BorrowRecords
                .Include(r => r.Book)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (record == null || record.Status == "returned")
                return BadRequest(new { message = "Invalid borrow record" });

            record.Status = "returned";
            record.ReturnDate = DateTime.UtcNow;

            record.Book.Quantity += 1;

            await _context.SaveChangesAsync();
            return Ok(record);
        }

        // Student only → My history
        [HttpGet("my")]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> MyBorrows()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier)
                            ?? User.FindFirstValue(ClaimTypes.Email)
                            ?? User.FindFirstValue(ClaimTypes.Name);

            if (string.IsNullOrEmpty(userEmail))
                return Unauthorized(new { message = "Invalid token: no email found" });

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            if (user == null)
                return Unauthorized(new { message = "User not found" });

            var records = await _context.BorrowRecords
                .Include(r => r.Book)
                .Where(r => r.UserId == user.Id)
                .ToListAsync();

            return Ok(records);
        }

        // Librarian only → All records
        [HttpGet("all")]
        [Authorize(Roles = "librarian")]
        public async Task<IActionResult> AllBorrows()
        {
            var records = await _context.BorrowRecords
                .Include(r => r.Book)
                .Include(r => r.User)
                .ToListAsync();

            return Ok(records);
        }
    }
}
