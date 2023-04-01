using AutoMapper;
using Project.NetflixApp.Dtos.ProductionCategoryDtos;
using Project.NetflixApp.Dtos.ProductionCommentDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Mapping.AutoMapper
{
    public class ProductionCommentProfile : Profile
    {
        public ProductionCommentProfile()
        {
            CreateMap<ProductionComment, CreateProductionCommentDto>().ReverseMap();
            CreateMap<ProductionComment, UpdateProductionCommentDto>().ReverseMap();
            CreateMap<ProductionComment, GetProductionCommentDto>().ReverseMap();
        }
    }
}
