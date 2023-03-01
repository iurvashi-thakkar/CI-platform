using System.ComponentModel.DataAnnotations;

namespace CI_platform.Models
{
    public class ResetPassword
    {
        public string Email { get; set; }
        public string Token { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
