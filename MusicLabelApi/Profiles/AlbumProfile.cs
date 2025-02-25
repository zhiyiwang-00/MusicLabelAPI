using AutoMapper;
using MusicLabelApi.Models;
using MusicLabelApi.Models.DTOs;

namespace MusicLabelApi.Profiles
{
    public class AlbumProfile : Profile
    {
        public AlbumProfile()
        {
            // Source -> Target
            CreateMap<Album, AlbumWithIdDTO>();
            CreateMap<AlbumDTO, Album>();
            CreateMap<Album, AlbumWithArtistsReadDTO>();
        }
    }
}