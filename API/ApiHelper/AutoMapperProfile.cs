using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.ApiHelper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, UserOutputDto>()
                .ForMember(dest => dest.PhotoUrl, option => option.MapFrom(src => src
                .Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.Age, option => option.MapFrom(src => src
                .DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotoOutputDto>();
        }
    }
}