using Library_Management_System.Models;

namespace Library_Management_System.DTO
{
    public class TransactionDTO
    {

        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime IssueDate { get; set; } // Ensure proper date binding
        public DateTime ReturnDate { get; set; }

        public virtual Book Book { get; set; }
        public virtual User User { get; set; }

        public int Quantity { get; set; }

        public string Title { get; set; }


        public string StudentName { get; set; }
    }
}
