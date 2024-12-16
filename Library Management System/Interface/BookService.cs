using Library_Management_System.Data;
using Library_Management_System.DTO;
using Library_Management_System.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Interface
{
    public class BookService : IBookService
    {
        private readonly LibraryContext _context;

        public BookService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<List<BookDTO>> GetAllBooks()
        {
            return await _context.Books.Select(b => new BookDTO
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                ISBN = b.ISBN,
                Quantity = b.Quantity
            }).ToListAsync();
        }

        public async Task<BookDTO> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            return new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                Quantity = book.Quantity
            };
        }

        public async Task AddBook(BookDTO bookDto)
        {
            var book = new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                ISBN = bookDto.ISBN,
                Quantity = bookDto.Quantity
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBook(int id, BookDTO bookDto)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                book.Title = bookDto.Title;
                book.Author = bookDto.Author;
                book.ISBN = bookDto.ISBN;
                book.Quantity = bookDto.Quantity;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<BookDTO>> SearchBooks(string title)
        {
            return await _context.Books
                .Where(b => b.Title.Contains(title))
                .Select(b => new BookDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    ISBN = b.ISBN,
                    Quantity = b.Quantity
                }).ToListAsync();
        }


        public async Task IssueBookAsync(TransactionDTO transactionDTO)
        {

            var book = await _context.Books.FindAsync(transactionDTO.Id);
            var user = await _context.Users.FindAsync(transactionDTO.UserId);
            if (book == null || book.Quantity < transactionDTO.Quantity)
            {
                throw new Exception("Book not available or insufficient quantity.");
            }

            var transaction = new Transaction
            {
                BookId = transactionDTO.Id,
                UserId = transactionDTO.UserId,
                IssueDate = transactionDTO.IssueDate,
                Quantity = transactionDTO.Quantity,
                StudentName = transactionDTO.StudentName,
                Title = transactionDTO.Title,   
                

            };

            book.Quantity -= transactionDTO.Quantity;
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task ReturnBook(int transactionId)
        {
            var transaction = await _context.Transactions.Include(t => t.Book).FirstOrDefaultAsync(t => t.Id == transactionId);
            if (transaction == null || transaction.ReturnDate != null)
            {
                throw new Exception("Invalid transaction or book already returned.");
            }

            transaction.ReturnDate = DateTime.Now;
            transaction.Book.Quantity += transaction.Quantity;
            await _context.SaveChangesAsync();
        }

        public async Task<List<TransactionDTO>> GetIssuedBooksByUser(int userId)
        {

            var transactions = await _context.Transactions.Where(t => t.UserId == userId && t.ReturnDate ==null).Include(t => t.Book).ToListAsync();

            return transactions.Select(t => new TransactionDTO
            {
                Id = t.Id,
                BookId = t.BookId,
                UserId = t.UserId,
                Title = t.Book.Title,
                IssueDate = t.IssueDate,
                Quantity = t.Quantity
            }).ToList();

        }


      


        public async Task<TransactionDTO> GetTransactionById(int Id)
        {
             
            var transaction = await _context.Transactions
                                            .Include(t => t.Book) 
                                            .Include(t => t.User)  
                                            .FirstOrDefaultAsync(t => t.Id == Id);  

            if (transaction == null)
            {
                return null; 
            }
 
            var transactionDto = new TransactionDTO
            {
                Id = transaction.Id,
                BookId = transaction.BookId,
                UserId = transaction.UserId,
                IssueDate = transaction.IssueDate,
                ReturnDate = transaction.ReturnDate ?? DateTime.Today,
                Book = transaction.Book,  
                User = transaction.User,  
                Quantity = transaction.Quantity,
                Title = transaction.Book?.Title,  
                StudentName = transaction.User?.Name 
            };

            return transactionDto;
        }

        public async Task<TransactionDTO> GetBookByIdd(int id)
        {
            var transaction = await _context.Books
             .Where(b => b.Id == id)
             .Select(b => new TransactionDTO
             {
                 BookId = b.Id,
                 Title = b.Title,
               //  Author = b.Author,
                 //Quantity = b.Quantity
             }).FirstOrDefaultAsync();

            if (transaction == null)
                throw new Exception("Book not found.");

            return transaction;
        }
    }

   
}


 