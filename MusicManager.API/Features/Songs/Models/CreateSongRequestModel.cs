using MusicManager.API.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace MusicManager.API.Features.Songs.Models
{
    public class CreateSongRequestModel
    {
        [Required]
        [MinLength(2, ErrorMessage = $"{nameof(Name)} shoult be betwenn 3 and 20 characters!")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int AlbumId { get; set; }
    }
}