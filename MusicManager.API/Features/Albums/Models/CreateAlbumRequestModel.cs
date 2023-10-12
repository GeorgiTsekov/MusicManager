using System.ComponentModel.DataAnnotations;

namespace MusicManager.API.Features.Albums.Models
{
    public class CreateAlbumRequestModel
    {
        [Required]
        [MinLength(2, ErrorMessage = $"{nameof(Name)} shoult be betwenn 3 and 20 characters!")]
        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Descritpion { get; set; }
        
        [Required]
        public int BandId { get; set; }
    }
}
