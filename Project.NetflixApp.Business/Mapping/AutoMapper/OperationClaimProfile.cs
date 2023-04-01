using AutoMapper;
using Project.NetflixApp.Dtos.CategoryDtos;
using Project.NetflixApp.Dtos.OperationClaimDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Mapping.AutoMapper
{
    public class OperationClaimProfile : Profile
    {
        public OperationClaimProfile()
        {
            CreateMap<OperationClaim, CreateOperationClaimDto>().ReverseMap();
            CreateMap<OperationClaim, UpdateOperationClaimDto>().ReverseMap();
            CreateMap<OperationClaim, GetOperationClaimDto>().ReverseMap();
        }
    }
}
