using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Business.Extensions;
using Project.NetflixApp.Business.Helpers.Constans;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Common.Utilities.Results.Concrete;
using Project.NetflixApp.DataAccess.Repositories.Abstract;
using Project.NetflixApp.Dtos.ProductionCommentDtos;
using Project.NetflixApp.Dtos.UserDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Concrete
{
    public class ProductionCommentManager : IProductionCommentService
    {
        private readonly IProductionCommentRepository _productionCommentRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateProductionCommentDto> _createProductionCommentDtoValidator;
        private readonly IValidator<UpdateProductionCommentDto> _updateProductionCommentDtoValidator;

        public ProductionCommentManager(IProductionCommentRepository productionCommentRepository, IMapper mapper, IValidator<CreateProductionCommentDto> createProductionCommentDtoValidator, IValidator<UpdateProductionCommentDto> updateProductionCommentDtoValidator)
        {
            _productionCommentRepository = productionCommentRepository;
            _mapper = mapper;
            _createProductionCommentDtoValidator = createProductionCommentDtoValidator;
            _updateProductionCommentDtoValidator = updateProductionCommentDtoValidator;
        }

        public async Task<IResponse> DeleteAsync(int id)
        {
            var data = await _productionCommentRepository.GetByIdAsync(id);
            if (data != null)
            {
                await _productionCommentRepository.DeleteAsync(data);
                return new Response(ResponseType.Success, ProductionCommentMessages.Deleted);
            }
            return new Response(ResponseType.NotFound, ProductionCommentMessages.NotDeleted);
        }

        public async Task<IDataResponse<IEnumerable<GetProductionCommentDto>>> GetAllAsync()
        {
            var entityData = await _productionCommentRepository.GetAllAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetProductionCommentDto>>(entityData);
            return new DataResponse<IEnumerable<GetProductionCommentDto>>(ResponseType.Success, mappingDto);
        }

        public async Task<IDataResponse<IEnumerable<GetProductionCommentDto>>> GetAllWithRelationsAsync()
        {
            var query = _productionCommentRepository.GetQuery();
            var entityData = await query.AsNoTracking().Include(x => x.User).ThenInclude(x => x.Gender).Include(x => x.Production).ThenInclude(x => x.TypeEntity).Include(x => x.Production).ThenInclude(x => x.Country).Include(x => x.Production).ThenInclude(x => x.Duraction).Include(x => x.Production).ThenInclude(x => x.Rating).Include(x => x.Production).ThenInclude(x => x.ProductionCategories).ThenInclude(x => x.Category).ToListAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetProductionCommentDto>>(entityData);
            //var dto = new List<GetProductionCommentDto>();
            //foreach (var entity in entityData)
            //{
            //    dto.Add(new GetProductionCommentDto
            //    {
            //        Id = entity.Id,
            //        Content = entity.Content,
            //        CreateDate = entity.CreateDate,
            //        UserId = entity.UserId,
            //        ProductionId = entity.ProductionId,
            //        User = new GetUserDto()
            //        {
            //            Id = entity.UserId,
            //            Email = entity.User.Email,
            //            Name = entity.User.Name,
            //            Lastname = entity.User.Lastname,
            //            ImageUrl = entity.User.ImageUrl,
            //            GenderId = entity.User.GenderId,
            //            Gender = new Dtos.GenderDtos.GetGenderDto()
            //            {
            //                Id = entity.User.Gender.Id,
            //                Definition = entity.User.Gender.Definition
            //            }
            //        },
            //        Production = new Dtos.ProductionDtos.GetProductionForComment()
            //        {
            //            Id = entity.ProductionId,
            //            Title = entity.Production.Title,
            //            Director = entity.Production.Director,
            //            Cast = entity.Production.Cast,
            //            ReleaseYear = entity.Production.ReleaseYear,
            //            CreateDate = entity.Production.CreateDate,
            //            TypeEntityId = entity.Production.TypeEntityId,
            //            DuractionId = entity.Production.DuractionId,
            //            CountryId = entity.Production.CountryId,
            //            RatingId = entity.Production.RatingId,
            //            TypeEntity = new Dtos.TypeEntityDtos.GetTypeEntityDto()
            //            {
            //                Id = entity.Production.TypeEntity.Id,
            //                Description = entity.Production.TypeEntity.Description
            //            },
            //            Duraction = new Dtos.DuractionDtos.GetDuractionDto()
            //            {
            //                Id = entity.Production.Duraction.Id,
            //                Description = entity.Production.Duraction.Description
            //            },
            //            Country = new Dtos.CountryDtos.GetCountryDto()
            //            {
            //                Id = entity.Production.Country.Id,
            //                Description= entity.Production.Country.Description
            //            },
            //            Rating = new Dtos.RatingDtos.GetRatingDto()
            //            {
            //                Id = entity.Production.Rating.Id,
            //                Description = entity.Production.Rating.Description
            //            },
            //        }
            //    });
            //}
            return new DataResponse<IEnumerable<GetProductionCommentDto>>(ResponseType.Success, mappingDto);
        }

        public async Task<IDataResponse<GetProductionCommentDto>> GetByIdAsync(int id)
        {
            var entityData = await _productionCommentRepository.GetByFilterAsync(x => x.Id == id);
            if (entityData != null)
            {
                var mappingDto = _mapper.Map<GetProductionCommentDto>(entityData);
                return new DataResponse<GetProductionCommentDto>(ResponseType.Success, mappingDto);
            }
            return new DataResponse<GetProductionCommentDto>(ResponseType.NotFound, $"{ProductionCommentMessages.NotFound}" + $"{id}");
        }

        public async Task<IDataResponse<GetProductionCommentDto>> GetByIdWithRelationsAsync(int id)
        {
            var query = _productionCommentRepository.GetQuery();
            var entityData = await query.Where(x => x.Id == id).AsNoTracking().Include(x => x.User).ThenInclude(x => x.Gender).Include(x => x.Production).ThenInclude(x => x.TypeEntity).Include(x => x.Production).ThenInclude(x => x.Country).Include(x => x.Production).ThenInclude(x => x.Duraction).Include(x => x.Production).ThenInclude(x => x.Rating).Include(x => x.Production).ThenInclude(x => x.ProductionCategories).ThenInclude(x => x.Category).FirstOrDefaultAsync();
            if(entityData != null)
            {
                var mappingDto = _mapper.Map<GetProductionCommentDto>(entityData);
                return new DataResponse<GetProductionCommentDto>(ResponseType.Success, mappingDto);
            }
            return new DataResponse<GetProductionCommentDto>(ResponseType.NotFound, $"{ProductionCommentMessages.NotFound}" + $"{id}");
        }

        public async Task<IResponse> InsertAsync(CreateProductionCommentDto createProductionCommentDto)
        {
            var validationResponse = _createProductionCommentDtoValidator.Validate(createProductionCommentDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<ProductionComment>(createProductionCommentDto);
                await _productionCommentRepository.InsertAsync(mappingEntity);
                return new Response(ResponseType.Success, ProductionCommentMessages.Created);
            }
            return new Response(ResponseType.ValidationError, validationResponse.ConvertToCustomValidationError());
        }

        public async Task<IResponse> UpdateAsync(UpdateProductionCommentDto updateProductionCommentDto)
        {
            var oldData = await _productionCommentRepository.AsNoTrackingGetByFilterAsync(x => x.Id == updateProductionCommentDto.Id);
            if (oldData != null)
            {
                var validationResponse = _updateProductionCommentDtoValidator.Validate(updateProductionCommentDto);
                if (validationResponse.IsValid)
                {
                    updateProductionCommentDto.CreateDate = oldData.CreateDate;
                    var mappingEntity = _mapper.Map<ProductionComment>(updateProductionCommentDto);
                    await _productionCommentRepository.UpdateAsync(mappingEntity);
                    return new Response(ResponseType.Success, ProductionCommentMessages.Updated);
                }
                return new Response(ResponseType.ValidationError, validationResponse.ConvertToCustomValidationError());
            }
            return new Response(ResponseType.NotFound, $"{ProductionCommentMessages.NotUpdated}" + $"{oldData.Id}");
        }
    }
}
