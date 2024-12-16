using Library_Management_System.Data;
using Library_Management_System.DTO;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Interface
{
    public class AdminLibraryService : IAdminLibrary
    {
        private readonly LibraryContext _context;
        private const string DefaultEmail = "navjeetsingh160@gmail.com";
        private const string DefaultPassword = "123";


        public AdminLibraryService(LibraryContext context)
        {
            _context = context;

        }

        public async Task<int> AddAdminLibrary(UserDTO userDTO)
        {
            var Admin = new User
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                Password = userDTO.Password,
            };

            _context.Users.Add(Admin);
            await _context.SaveChangesAsync();

            // Return the generated UserId
            return Admin.Id;
        }


        public async Task<int?> LoginAsync(UserDTO userDTO)
        {
            var admin = await _context.Users
                .FirstOrDefaultAsync(admin => admin.Email == userDTO.Email);

            if (admin != null && admin.Password == userDTO.Password)  
            {
                
                return  admin.Id;


            }

            return null; 
        }

     
        public async Task LogoutAsync()
        {
             
        }


        public Task<bool> TeacherLoginAuthenticate(UserDTO userDTO)
        {
            // Simple authentication logic
            var isAuthenticated = userDTO.Email == DefaultEmail && userDTO.Password == DefaultPassword;
            return Task.FromResult(isAuthenticated);
        }
    }
}
