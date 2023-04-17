using AutoMapper;
using Project.NetflixApp.Dtos.UserDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Mapping.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
            CreateMap<User, GetUserDto>().ReverseMap();
            CreateMap<User, LoginUserDto>().ReverseMap();
            CreateMap<User, RegisterUserDto>().ReverseMap();
        }
    }
}
