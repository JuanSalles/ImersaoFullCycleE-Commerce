using System.ComponentModel.DataAnnotations;

namespace AuthServer.Application.DTOs

{
    public class loginDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}