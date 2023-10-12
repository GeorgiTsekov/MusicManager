using MusicManager.API.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MusicManager.API.Features.Musicians.Models
{
    public class CreateMusicianRequestModel
    {
        [Required]
        [MinLength(2, ErrorMessage = $"{nameof(Name)} shoult be betwenn 3 and 20 characters!")]
        [MaxLength(20)]
        public string Name { get; set; }

        [EnumDataType(typeof(DailyRent))]
        public DailyRent DailyRent { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int BandId { get; set; }
    }
}
