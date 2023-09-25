using AutoMapper;
using MusicManager.API.Data.Models;
using MusicManager.API.Features.Albums.Models;
using MusicManager.API.Features.Bands.Models;
using MusicManager.API.Features.Musicians.Models;
using MusicManager.API.Features.Songs.Models;

namespace MusicManager.API.Features
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Band, BandModel>().ReverseMap();
            CreateMap<Band, BandDetails>().ReverseMap();
            CreateMap<CreateBandRequestModel, Band>().ReverseMap();
            CreateMap<UpdateBandRequestModel, Band>().ReverseMap();
            CreateMap<Musician, MusicianModel>().ReverseMap();
            CreateMap<CreateMusicianRequestModel, Musician>().ReverseMap();
            CreateMap<UpdateMusicianRequestModel, Musician>().ReverseMap();
            CreateMap<Album, AlbumModel>().ReverseMap();
            CreateMap<Album, AlbumDetails>().ReverseMap();
            CreateMap<CreateAlbumRequestModel, Album>().ReverseMap();
            CreateMap<UpdateAlbumRequestModel, Album>().ReverseMap();
            CreateMap<Song, SongModel>().ReverseMap();
            CreateMap<Song, SongDetails>().ReverseMap();
            CreateMap<CreateSongRequestModel, Song>().ReverseMap();
            CreateMap<UpdateSongRequestModel, Song>().ReverseMap();
        }
    }
}
