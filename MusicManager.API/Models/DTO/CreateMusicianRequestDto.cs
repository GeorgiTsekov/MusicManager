using MusicManager.API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace MusicManager.API.Models.DTO
{
    public class CreateMusicianRequestDto
    {
        [Required]
        [MinLength(2, ErrorMessage = $"{nameof(Name)} shoult be betwenn 3 and 20 characters!")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Range(1, 5)]
        public MusicalInstrumentType MusicalInstrumentType { get; set; }

        [Range(1, 5)]
        public Clothing Clothing { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int BandId { get; set; }
    }
}
