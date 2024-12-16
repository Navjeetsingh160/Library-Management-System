using Library_Management_System.DTO;

namespace Library_Management_System.Interface
{
    public interface IAdminLibrary
    {

        Task<int> AddAdminLibrary (UserDTO  userDTO);
        Task<int?> LoginAsync(UserDTO userDTO);
        Task<bool> TeacherLoginAuthenticate(UserDTO userDTO);
        Task LogoutAsync();
    }
}
