using System;

namespace LibraryManagementSystem.DTOs
{
    public class BorrowRequestDto
    {
        public int BookId { get; set; }
    }

    public class ReturnRequestDto
    {
        public int BorrowRecordId { get; set; }
    }

    public class BorrowResponseDto
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public string Status { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
