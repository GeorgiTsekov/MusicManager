using System.ComponentModel.DataAnnotations;

namespace MusicManager.API.Features.Users.Models
{
    public class UserDetailsServiceModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        public decimal Money { get; set; }
    }
}
