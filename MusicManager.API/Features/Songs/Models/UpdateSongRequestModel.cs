using System.ComponentModel.DataAnnotations;

namespace MusicManager.API.Features.Songs.Models
{
    public class UpdateSongRequestModel
    {
        [Required]
        [MinLength(2, ErrorMessage = $"{nameof(Name)} shoult be betwenn 3 and 20 characters!")]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}