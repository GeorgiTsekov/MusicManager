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
            CreateMap<UpdateBandRequestDto, Band>().ReverseMap();

            //CreateMap<Band, BandDto>()
            //    .ForMember(x => x.Name, opt => opt.MapFrom(x => x.FullName))
            //    .ReverseMap();
        }
    }
}
