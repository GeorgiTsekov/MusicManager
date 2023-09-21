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
            CreateMap<Band, BandSingleDto>().ReverseMap();
            CreateMap<CreateBandRequestDto, Band>().ReverseMap();
            CreateMap<UpdateBandRequestDto, Band>().ReverseMap();
            CreateMap<Musician, MusicianDto>().ReverseMap();
            CreateMap<CreateMusicianRequestDto, Musician>().ReverseMap();
            CreateMap<UpdateMusicianRequestDto, Musician>().ReverseMap();
        }
    }
}
