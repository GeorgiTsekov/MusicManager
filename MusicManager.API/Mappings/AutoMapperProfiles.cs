using AutoMapper;
using MusicManager.API.Models.Domain;
using MusicManager.API.Models.DTO;

namespace MusicManager.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Band, BandDto>().ReverseMap();
            CreateMap<CreateBandRequestDto, Band>().ReverseMap();
        }
    }
}
