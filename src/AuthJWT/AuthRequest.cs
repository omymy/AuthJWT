using System.ComponentModel.DataAnnotations;

namespace AuthJWT
{
    public class AuthRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
