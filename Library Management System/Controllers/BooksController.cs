using Library_Management_System.DTO;
using Library_Management_System.Interface;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace Library_Management_System.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooks();
            return View(books);
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetBookById(id);
            return View(book);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookDTO bookDto)
        {
            if (ModelState.IsValid)
            {
                await _bookService.AddBook(bookDto);
                return RedirectToAction(nameof(Index));
            }
            return View(bookDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookService.GetBookById(id);
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookDTO bookDto)
        {
            if (ModelState.IsValid)
            {
                await _bookService.UpdateBook(id, bookDto);
                return RedirectToAction(nameof(Index));
            }
            return View(bookDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteBook(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Search(string title)
        {
            var books = await _bookService.SearchBooks(title);
            return View("Index", books);  
        }

        public async Task<IActionResult> IndexIssueBook(BookDTO bookDto)
        {
            var books = await _bookService.GetAllBooks();
            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> IssueBook(int Id)
        {

            var transactionDto = await _bookService.GetBookByIdd(Id);



             

            return View(transactionDto);
        }

        [HttpPost]
        public async Task<IActionResult> IssueBook(TransactionDTO transactionDTO)
        {
            try
            {

                 
                int? userId = HttpContext.Session.GetInt32("UserId");
                transactionDTO.UserId = userId.Value;
                await _bookService.IssueBookAsync(transactionDTO);
                // HttpContext.Session.SetInt32("BookId", transactionDTO.BookId);

               

                return RedirectToAction("IndexIssueBook", "Books");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(transactionDTO);
            }
        }


        [HttpGet]
        public async Task<IActionResult> UserIndexBook()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            var issuedBooks = await _bookService.GetIssuedBooksByUser(userId.Value);
            if (issuedBooks.Count > 0)
            {
                var firstBookId = issuedBooks.First().BookId;  
                HttpContext.Session.SetInt32("BookId", firstBookId);  
            }
            return View(issuedBooks);
        }




        [HttpPost]
         public async Task<IActionResult> UserIndexBook(int userId, int transactionId)
        {
              try
              {
                  await _bookService.ReturnBook(transactionId);
                  return RedirectToAction("IndexIssueBook", "Books");
              }
              catch (Exception ex)
              {
                 ModelState.AddModelError(string.Empty, ex.Message);
                 return View();
             }
         }





        [HttpGet]
        public async Task<IActionResult> ReturnBook(int Id)
        {
 
              var transactionDto = await _bookService.GetTransactionById(Id);
 

           
              ViewBag.TransactionId = transactionDto.Id;
 
            return View(transactionDto);  
        }




        [HttpPost]
        public async Task<IActionResult> ReturnBook(TransactionDTO transactionDto)
        {
            try
            {
                await _bookService.ReturnBook(transactionDto.Id);
                return RedirectToAction("IndexIssueBook", "Books");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }
    }
}