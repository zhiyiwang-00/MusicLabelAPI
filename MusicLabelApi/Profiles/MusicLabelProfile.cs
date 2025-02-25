using AutoMapper;
using MusicLabelApi.Models;
using MusicLabelApi.Models.DTOs;

namespace MusicLabelApi.Profiles
{
    public class MusicLabelProfile : Profile
    {
        public MusicLabelProfile()
        {
            // Source -> Target
            CreateMap<MusicLabel, MusicLabelWithIdDTO>();
            CreateMap<MusicLabelDTO, MusicLabel>();
            CreateMap<MusicLabel, MusicLabelDTO>();
        }
    }
}