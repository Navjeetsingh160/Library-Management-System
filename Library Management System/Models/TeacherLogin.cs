using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Models
{
    public class TeacherLogin
    {
        [Key]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
