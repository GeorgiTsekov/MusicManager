using MusicManager.API.Common.Models;
using MusicManager.API.Models.Enums;

namespace MusicManager.API.Models.DTO
{
    public class BandDto : BaseDeletableModel<int>
    {
        public string Name { get; set; }
        public Style Style { get; set; }
        public double Money { get; set; }
    }
}
