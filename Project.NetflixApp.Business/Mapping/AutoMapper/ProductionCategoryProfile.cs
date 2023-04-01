using AutoMapper;
using Project.NetflixApp.Dtos.OperationClaimDtos;
using Project.NetflixApp.Dtos.ProductionCategoryDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Mapping.AutoMapper
{
    public class ProductionCategoryProfile : Profile
    {
        public ProductionCategoryProfile()
        {
            CreateMap<ProductionCategory, CreateProductionCategoryDto>().ReverseMap();
            CreateMap<ProductionCategory, UpdateProductionCategoryDto>().ReverseMap();
            CreateMap<ProductionCategory, GetProductionCategoryDto>().ReverseMap();
        }
    }
}
