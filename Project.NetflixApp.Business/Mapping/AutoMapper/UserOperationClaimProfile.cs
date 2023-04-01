using AutoMapper;
using Project.NetflixApp.Dtos.TypeEntityDtos;
using Project.NetflixApp.Dtos.UserOperationClaimDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Mapping.AutoMapper
{
    public class UserOperationClaimProfile : Profile
    {
        public UserOperationClaimProfile()
        {
            CreateMap<UserOperationClaim, CreateUserOperationClaimDto>().ReverseMap();
            CreateMap<UserOperationClaim, UpdateUserOperationClaimDto>().ReverseMap();
            CreateMap<UserOperationClaim, GetUserOperationClaimDto>().ReverseMap();
        }
    }
}
