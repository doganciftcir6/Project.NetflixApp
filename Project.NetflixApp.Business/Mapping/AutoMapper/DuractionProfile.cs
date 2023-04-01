using AutoMapper;
using Project.NetflixApp.Dtos.CategoryDtos;
using Project.NetflixApp.Dtos.DuractionDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Mapping.AutoMapper
{
    public class DuractionProfile : Profile
    {
        public DuractionProfile()
        {
            CreateMap<Duraction, CreateDuractionDto>().ReverseMap();
            CreateMap<Duraction, UpdateDuractionDto>().ReverseMap();
            CreateMap<Duraction, GetDuractionDto>().ReverseMap();
        }
    }
}
