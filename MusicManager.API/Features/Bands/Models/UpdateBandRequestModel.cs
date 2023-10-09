using MusicManager.API.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MusicManager.API.Features.Bands.Models
{
    public class UpdateBandRequestModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "Name shoult be betwenn 3 and 20 characters!")]
        [MaxLength(20)]
        public string Name { get; set; }

        public string Style { get; set; }
    }
}
