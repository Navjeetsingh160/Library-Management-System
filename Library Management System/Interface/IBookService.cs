using Library_Management_System.DTO;

namespace Library_Management_System.Interface
{
    public interface IBookService
    {
        Task<List<BookDTO>> GetAllBooks();
        Task<BookDTO> GetBookById(int id);
        Task AddBook(BookDTO bookDto);
        Task UpdateBook(int id, BookDTO bookDto);
        Task DeleteBook(int id);
        Task<List<BookDTO>> SearchBooks(string title);

        Task IssueBookAsync(TransactionDTO transactionDTO);
        Task ReturnBook(int transactionId);

        Task<List<TransactionDTO>> GetIssuedBooksByUser(int userId);
        Task<TransactionDTO> GetTransactionById(int transactionId);

        Task<TransactionDTO> GetBookByIdd(int id);

    }
}
