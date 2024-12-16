using Library_Management_System.DTO;
using Library_Management_System.Interface;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    public class AddAdminLibraryController : Controller
    {

        public readonly IAdminLibrary _adminLibrary;

        public AddAdminLibraryController(IAdminLibrary adminLibrary)
        {
            _adminLibrary = adminLibrary;
            
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminLibraryCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminLibraryCreate(UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                
                int userId = await _adminLibrary.AddAdminLibrary(userDTO);

                HttpContext.Session.SetInt32("UserId", userId);


            
                TempData["Message"] = $"Admin created successfully with UserId: {userId}";

                return RedirectToAction("IndexIssueBook", "Books", new { userId = userId });
            }

            return View(userDTO);
        }


        [HttpPost]
        public async Task<IActionResult> Login(UserDTO userDTO)
        {
            var userId = await _adminLibrary.LoginAsync(userDTO);

            if (userId.HasValue)  
            {
                HttpContext.Session.SetInt32("UserId", userId.Value);
               
                return RedirectToAction("UserIndexBook", "Books", new { userId = userId });
            }

 


            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View("Index", userDTO);  
        }


     
        public async Task<IActionResult> Logout()
        {
            await _adminLibrary.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public IActionResult TeacherLogin()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> TeacherLogin(UserDTO userDTO)
        {
            var isAuthenticated = await _adminLibrary.TeacherLoginAuthenticate(userDTO);

            if (isAuthenticated)
            {
                TempData["LoginMessage"] = "success";
                TempData["MessageContent"] = "Welcome back! Login successful.";
                return RedirectToAction("Index", "Books");
            }

            TempData["LoginMessage"] = "error";
            TempData["MessageContent"] = "Invalid login credentials. Please try again.";
            return View(userDTO);
        }



    }
}
