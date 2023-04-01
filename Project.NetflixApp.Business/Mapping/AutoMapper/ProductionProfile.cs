using AutoMapper;
using Project.NetflixApp.Dtos.ProductionCommentDtos;
using Project.NetflixApp.Dtos.ProductionDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Mapping.AutoMapper
{
    public class ProductionProfile : Profile
    {
        public ProductionProfile()
        {
            CreateMap<Production, CreateProductionDto>().ReverseMap();
            CreateMap<Production, UpdateProductionDto>().ReverseMap();
            CreateMap<Production, GetProductionDto>().ReverseMap();
        }
    }
}
