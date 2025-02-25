using AutoMapper;
using MusicLabelApi.Models;
using MusicLabelApi.Models.DTOs;

namespace MusicLabelApi.Profiles
{
    public class ArtistProfile : Profile
    {
        public ArtistProfile()
        {
            // Source -> Target
            CreateMap<Artist, ArtistDTO>();
            CreateMap<ArtistDTO, Artist>();
            CreateMap<Artist, ArtistWithIdDTO>();
            CreateMap<Artist, ArtistSimpleReadDTO>();
        }
    }
}
