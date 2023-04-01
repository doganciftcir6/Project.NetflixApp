using AutoMapper;
using Project.NetflixApp.Dtos.CategoryDtos;
using Project.NetflixApp.Dtos.GenderDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Mapping.AutoMapper
{
    public class GenderProfile : Profile
    {
        public GenderProfile()
        {
            CreateMap<Gender, CreateGenderDto>().ReverseMap();
            CreateMap<Gender, UpdateGenderDto>().ReverseMap();
            CreateMap<Gender, GetGenderDto>().ReverseMap();
        }
    }
}
