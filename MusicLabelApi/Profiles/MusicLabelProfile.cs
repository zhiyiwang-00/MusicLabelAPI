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
            CreateMap<MusicLabel, MusicLabelReadDTO>();
            CreateMap<MusicLabelCreateDTO, MusicLabel>();
            CreateMap<MusicLabelUpdateDTO, MusicLabel>();
        }
    }
}