using MusicManager.API.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MusicManager.API.Features.Bands.Models
{
    public class CreateBandRequestDto
    {
        [Required]
        [MinLength(2, ErrorMessage = $"{nameof(Name)} shoult be betwenn 3 and 20 characters!")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Range(1, 3)]
        public Style Style { get; set; }
    }
}
