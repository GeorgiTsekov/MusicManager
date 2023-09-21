using MusicManager.API.Common.Models;
using MusicManager.API.Models.Enums;

namespace MusicManager.API.Models.DTO
{
    public class BandSingleDto : BaseDeletableModel<int>
    {
        public string Name { get; set; }
        public Style Style { get; set; }
        public int Energy { private get; set; }
        public virtual ICollection<MusicianDto> Musicians { get; set; }
        public virtual ICollection<MusicianDto> Albums { get; set; }
    }
}
