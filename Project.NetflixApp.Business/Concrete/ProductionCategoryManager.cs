using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Business.Extensions;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Common.Utilities.Results.Concrete;
using Project.NetflixApp.DataAccess.Repositories.Abstract;
using Project.NetflixApp.Dtos.ProductionCategoryDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Concrete
{
    public class ProductionCategoryManager : IProductionCategoryService
    {
        private readonly IProductionCategoryRepository _productionCategoryRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateProductionCategoryDto> _createProductionCategoryDtoValidator;
        private readonly IValidator<UpdateProductionCategoryDto> _updateProductionCategoryDtoValidator;

        public ProductionCategoryManager(IProductionCategoryRepository productionCategoryRepository, IMapper mapper, IValidator<CreateProductionCategoryDto> createProductionCategoryDtoValidator, IValidator<UpdateProductionCategoryDto> updateProductionCategoryDtoValidator)
        {
            _productionCategoryRepository = productionCategoryRepository;
            _mapper = mapper;
            _createProductionCategoryDtoValidator = createProductionCategoryDtoValidator;
            _updateProductionCategoryDtoValidator = updateProductionCategoryDtoValidator;
        }

        public async Task<IResponse> DeleteAsync(int id)
        {
            var data = await _productionCategoryRepository.GetByIdAsync(id);
            if (data != null)
            {
                await _productionCategoryRepository.DeleteAsync(data);
                return new Response(ResponseType.Success, "The productioncategory was successfully deleted");
            }
            return new Response(ResponseType.NotFound, "The productioncategory parameter could not be deleted because the productioncategory could not be found.");
        }

        public async Task<IDataResponse<IEnumerable<GetProductionCategoryDto>>> GetAllAsync()
        {
            var entityData = await _productionCategoryRepository.GetAllAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetProductionCategoryDto>>(entityData);
            return new DataResponse<IEnumerable<GetProductionCategoryDto>>(ResponseType.Success, mappingDto);
        }

        public async Task<IDataResponse<IEnumerable<GetProductionCategoryDto>>> GetAllWithRelationsAsync()
        {
            var query = _productionCategoryRepository.GetQuery();
            var entityData = await query.AsNoTracking().Include(x => x.Production).ThenInclude(x => x.TypeEntity).Include(x => x.Production).ThenInclude(x => x.Duraction).Include(x => x.Production).ThenInclude(x => x.Country).Include(X => X.Production).ThenInclude(x => x.Rating).Include(x => x.Category).ToListAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetProductionCategoryDto>>(entityData);
            return new DataResponse<IEnumerable<GetProductionCategoryDto>>(ResponseType.Success, mappingDto);
        }

        public async Task<IDataResponse<GetProductionCategoryDto>> GetByIdAsync(int id)
        {
            var entityData = await _productionCategoryRepository.GetByFilterAsync(x => x.Id == id);
            if (entityData != null)
            {
                var mappingDto = _mapper.Map<GetProductionCategoryDto>(entityData);
                return new DataResponse<GetProductionCategoryDto>(ResponseType.Success, mappingDto);
            }
            return new DataResponse<GetProductionCategoryDto>(ResponseType.NotFound, $"The related productioncategory could not be found. Productioncategory Id:");
        }

        public async Task<IDataResponse<GetProductionCategoryDto>> GetByIdWithRelationsAsync(int id)
        {
            var query = _productionCategoryRepository.GetQuery();
            var entityData = await query.Where(x => x.Id == id).AsNoTracking().Include(x => x.Production).ThenInclude(x => x.TypeEntity).Include(x => x.Production).ThenInclude(x => x.Duraction).Include(x => x.Production).ThenInclude(x => x.Country).Include(X => X.Production).ThenInclude(x => x.Rating).Include(x => x.Category).FirstOrDefaultAsync();
            if (entityData != null)
            {
                var mappingDto = _mapper.Map<GetProductionCategoryDto>(entityData);
                return new DataResponse<GetProductionCategoryDto>(ResponseType.Success, mappingDto);
            }
            return new DataResponse<GetProductionCategoryDto>(ResponseType.NotFound, $"The related productioncategory could not be found. Productioncategory Id:");
        }

        public async Task<IResponse> InsertAsync(CreateProductionCategoryDto createProductionCategoryDto)
        {
            var validationResponse = _createProductionCategoryDtoValidator.Validate(createProductionCategoryDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<ProductionCategory>(createProductionCategoryDto);
                await _productionCategoryRepository.InsertAsync(mappingEntity);
                return new Response(ResponseType.Success, "The productioncategory adding process has been successfully completed.");
            }
            return new Response(ResponseType.ValidationError, validationResponse.ConvertToCustomValidationError());
        }

        public async Task<IResponse> UpdateAsync(UpdateProductionCategoryDto updateProductionCategoryDto)
        {
            var oldData = await _productionCategoryRepository.AsNoTrackingGetByFilterAsync(x => x.Id == updateProductionCategoryDto.Id);
            if (oldData != null)
            {
                var validationResponse = _updateProductionCategoryDtoValidator.Validate(updateProductionCategoryDto);
                if (validationResponse.IsValid)
                {
                    var mappingEntity = _mapper.Map<ProductionCategory>(updateProductionCategoryDto);
                    await _productionCategoryRepository.UpdateAsync(mappingEntity);
                    return new Response(ResponseType.Success, "The productioncategory updating process has been successfully completed.");
                }
                return new Response(ResponseType.ValidationError, validationResponse.ConvertToCustomValidationError());
            }
            return new Response(ResponseType.NotFound, "The related productioncategory could not be found. So the update process could not be completed. Productioncategory Id:");
        }
    }
}
