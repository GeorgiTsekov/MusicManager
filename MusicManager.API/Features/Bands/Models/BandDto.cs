using MusicManager.API.Common.Models;
using MusicManager.API.Data.Enums;

namespace MusicManager.API.Features.Bands.Models
{
    public class BandDto : BaseDeletableModel<int>
    {
        public string Name { get; set; }
        public Style Style { get; set; }
        public int Energy { private get; set; }
    }
}
