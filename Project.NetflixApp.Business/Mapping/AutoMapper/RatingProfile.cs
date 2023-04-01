using AutoMapper;
using Project.NetflixApp.Dtos.ProductionCommentDtos;
using Project.NetflixApp.Dtos.RatingDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Mapping.AutoMapper
{
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            CreateMap<Rating, CreateRatingDto>().ReverseMap();
            CreateMap<Rating, UpdateRatingDto>().ReverseMap();
            CreateMap<Rating, GetRatingDto>().ReverseMap();
        }
    }
}
