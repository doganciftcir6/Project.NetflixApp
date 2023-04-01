using AutoMapper;
using Project.NetflixApp.Dtos.ProductionCommentDtos;
using Project.NetflixApp.Dtos.TypeEntityDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Mapping.AutoMapper
{
    public class TypeEntityProfile : Profile
    {
        public TypeEntityProfile()
        {
            CreateMap<TypeEntity, CreateTypeEntityDto>().ReverseMap();
            CreateMap<TypeEntity, UpdateTypeEntityDto>().ReverseMap();
            CreateMap<TypeEntity, GetTypeEntityDto>().ReverseMap();
        }
    }
}
