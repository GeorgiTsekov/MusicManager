using MusicManager.API.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace MusicManager.API.Features.Bands.Models
{
    public class BandModel : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }

        public string Style { get; set; }

        public int Energy { private get; set; }
    }
}
