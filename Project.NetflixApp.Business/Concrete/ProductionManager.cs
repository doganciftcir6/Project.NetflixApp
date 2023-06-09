﻿using AutoMapper;
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
using Project.NetflixApp.Dtos.ProductionDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Concrete
{
    public class ProductionManager : IProductionService
    {
        private readonly IProductionRepository _productionRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateProductionDto> _createProductionDtoValidator;
        private readonly IValidator<UpdateProductionDto> _updateProductionDtoValidator;

        public ProductionManager(IProductionRepository productionRepository, IMapper mapper, IValidator<CreateProductionDto> createProductionDtoValidator, IValidator<UpdateProductionDto> updateProductionDtoValidator)
        {
            _productionRepository = productionRepository;
            _mapper = mapper;
            _createProductionDtoValidator = createProductionDtoValidator;
            _updateProductionDtoValidator = updateProductionDtoValidator;
        }

        public async Task<IResponse> DeleteAsync(int id)
        {
            var data = await _productionRepository.GetByIdAsync(id);
            if (data != null)
            {
                await _productionRepository.DeleteAsync(data);
                return new Response(ResponseType.Success, ProductionMessages.Deleted);
            }
            return new Response(ResponseType.NotFound, ProductionMessages.NotDeleted);
        }

        public async Task<IDataResponse<IEnumerable<GetProductionDto>>> GetAllAsync()
        {
            var entityData = await _productionRepository.GetAllAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetProductionDto>>(entityData);
            return new DataResponse<IEnumerable<GetProductionDto>>(ResponseType.Success, mappingDto);
        }

        public async Task<IDataResponse<IEnumerable<GetProductionDto>>> GetAllWithRelationsAsync()
        {
            var query = _productionRepository.GetQuery();
            var data = await query.AsNoTracking().Include(x => x.TypeEntity).Include(x => x.Country).Include(x => x.Rating).Include(x => x.Duraction).Include(x => x.ProductionCategories).ThenInclude(x => x.Category).Include(x => x.ProductionComments).ThenInclude(x => x.User).ThenInclude(x => x.Gender).ToListAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetProductionDto>>(data);
            return new DataResponse<IEnumerable<GetProductionDto>>(ResponseType.Success, mappingDto);
        }

        public async Task<IDataResponse<GetProductionDto>> GetByIdAsync(int id)
        {
            var entityData = await _productionRepository.GetByFilterAsync(x => x.Id == id);
            if (entityData != null)
            {
                var mappingDto = _mapper.Map<GetProductionDto>(entityData);
                return new DataResponse<GetProductionDto>(ResponseType.Success, mappingDto);
            }
            return new DataResponse<GetProductionDto>(ResponseType.NotFound, $"{ProductionMessages.NotFound}" + $"{id}");
        }

        public async Task<IDataResponse<GetProductionDto>> GetByIdWithRelationsAsync(int id)
        {
            var query = _productionRepository.GetQuery();
            var entityData = await query.Where(x => x.Id == id).AsNoTracking().Include(x => x.TypeEntity).Include(x => x.Country).Include(x => x.Rating).Include(x => x.Duraction).Include(x => x.ProductionCategories).ThenInclude(x => x.Category).Include(x => x.ProductionComments).ThenInclude(x => x.User).ThenInclude(x => x.Gender).FirstOrDefaultAsync();
            if (entityData != null)
            {
                var mappingDto = _mapper.Map<GetProductionDto>(entityData);
                return new DataResponse<GetProductionDto>(ResponseType.Success, mappingDto);
            }
            return new DataResponse<GetProductionDto>(ResponseType.NotFound, $"{ProductionMessages.NotFound}" + $"{id}");
        }

        public async Task<IResponse> InsertAsync(CreateProductionDto createProductionDto)
        {
            var validationResponse = _createProductionDtoValidator.Validate(createProductionDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<Production>(createProductionDto);
                await _productionRepository.InsertAsync(mappingEntity);
                return new Response(ResponseType.Success, ProductionMessages.Created);
            }
            return new Response(ResponseType.ValidationError, validationResponse.ConvertToCustomValidationError());
        }

        public async Task<IResponse> UpdateAsync(UpdateProductionDto updateProductionDto)
        {
            var oldData = await _productionRepository.AsNoTrackingGetByFilterAsync(x => x.Id == updateProductionDto.Id);
            if (oldData != null)
            {
                var validationResponse = _updateProductionDtoValidator.Validate(updateProductionDto);
                if (validationResponse.IsValid)
                {
                    updateProductionDto.CreateDate = oldData.CreateDate;
                    var mappingEntity = _mapper.Map<Production>(updateProductionDto);
                    await _productionRepository.UpdateAsync(mappingEntity);
                    return new Response(ResponseType.Success, ProductionMessages.Updated);
                }
                return new Response(ResponseType.ValidationError, validationResponse.ConvertToCustomValidationError());
            }
            return new Response(ResponseType.NotFound, $"{ProductionMessages.NotFound}" + $"{oldData.Id}");
        }
    }
}
