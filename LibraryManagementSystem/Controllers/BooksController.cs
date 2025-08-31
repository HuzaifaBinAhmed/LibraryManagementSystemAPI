using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        // Librarian only → Add book
        // Example for POST /api/books
        [HttpPost]
        [Authorize(Roles = "librarian")]
        public async Task<IActionResult> AddBook(Book book)
        {
            if (book.Quantity < 0)
                return BadRequest(new { message = "Quantity cannot be negative." });

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return Ok(book);
        }

        // Example for PUT /api/books/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "librarian")]
        public async Task<IActionResult> UpdateBook(int id, Book updatedBook)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound(new { message = "Book not found." });

            if (updatedBook.Quantity < 0)
                return BadRequest(new { message = "Quantity cannot be negative." });

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.ISBN = updatedBook.ISBN;
            book.Category = updatedBook.Category;
            book.Quantity = updatedBook.Quantity;
            book.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(book);
        }


        // Librarian only → Delete book
        [HttpDelete("{id}")]
        [Authorize(Roles = "librarian")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Book deleted" });
        }
    }
}
