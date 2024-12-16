namespace Library_Management_System.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int Quantity { get; set; }

        // Navigation Property for Transactions
        public virtual ICollection<Transaction> Transactions { get; set; }

    }
}
