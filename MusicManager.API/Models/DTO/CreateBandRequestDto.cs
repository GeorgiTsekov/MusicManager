using MusicManager.API.Models.Enums;

namespace MusicManager.API.Models.DTO
{
    public class CreateBandRequestDto
    {
        public string Name { get; set; }
        public Style Style { get; set; }
    }
}
