using AutoMapper;
using Project.NetflixApp.Dtos.ProductionCategoryDtos;
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
            CreateMap<GetProductionDto, Production>().ReverseMap()
                .ForMember(x => x.Categories, opt => opt.MapFrom(y => y.ProductionCategories.Select(x => x.Category)));
            CreateMap<GetProductionForComment, Production>().ReverseMap()
               .ForMember(x => x.Categories, opt => opt.MapFrom(y => y.ProductionCategories.Select(x => x.Category)));
        }
    }
}
