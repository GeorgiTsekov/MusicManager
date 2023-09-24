using AutoMapper;
using MusicManager.API.Data.Models;
using MusicManager.API.Features.Bands.Models;
using MusicManager.API.Features.Musicians.Models;

namespace MusicManager.API.Features
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
