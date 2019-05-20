using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class UserDetails
    {
        public string Password { get; set; }

        [StringLength(7, MinimumLength = 2, ErrorMessage = "UserName length should be between 2 and 7")]
        public string UserName { get; set; }
    }
}
