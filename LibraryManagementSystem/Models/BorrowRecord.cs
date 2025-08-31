using System.ComponentModel.DataAnnotations;
namespace LibraryManagementSystem.Models
{
    public class BorrowRecord
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        [Required]
        [RegularExpression("^(borrowed|returned)$", ErrorMessage = "Status must be 'borrowed' or 'returned'.")]
        public string Status { get; set; } = "borrowed";

        [Required]
        public DateTime DueDate { get; set; }

        // Navigation props (optional)
        public User? User { get; set; }
        public Book? Book { get; set; }
    }
}