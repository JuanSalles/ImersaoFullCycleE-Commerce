using System.ComponentModel.DataAnnotations;

namespace AuthServer.Application.DTOs
{
    public class CreateUserDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string RePassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Email { get; set; }
    }
}