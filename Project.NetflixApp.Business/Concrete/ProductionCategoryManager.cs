using AutoMapper;
using FluentValidation;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.DataAccess.Repositories.Abstract;
using Project.NetflixApp.Dtos.ProductionCategoryDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task DeleteAsync(int id)
        {
            var data = await _productionCategoryRepository.GetByIdAsync(id);
            if(data != null)
            {
                await _productionCategoryRepository.DeleteAsync(data);
            }
        }

        public async Task<IEnumerable<GetProductionCategoryDto>> GetAllAsync()
        {
            var entityData = await _productionCategoryRepository.GetAllAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetProductionCategoryDto>>(entityData);
            return mappingDto;
        }

        public async Task<GetProductionCategoryDto> GetByIdAsync(int id)
        {
            var entityData = await _productionCategoryRepository.GetByFilterAsync(x => x.Id == id);
            if(entityData != null)
            {
                var mappingDto = _mapper.Map<GetProductionCategoryDto>(entityData);
                return mappingDto;
            }
            return null;
        }

        public async Task<CreateProductionCategoryDto> InsertAsync(CreateProductionCategoryDto createProductionCategoryDto)
        {
            var validationResponse = _createProductionCategoryDtoValidator.Validate(createProductionCategoryDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<ProductionCategory>(createProductionCategoryDto);
                await _productionCategoryRepository.InsertAsync(mappingEntity);
                return createProductionCategoryDto;
            }
            return null;
        }

        public async Task<UpdateProductionCategoryDto> UpdateAsync(UpdateProductionCategoryDto updateProductionCategoryDto)
        {
            var validationResponse = _updateProductionCategoryDtoValidator.Validate(updateProductionCategoryDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<ProductionCategory>(updateProductionCategoryDto);
                await _productionCategoryRepository.UpdateAsync(mappingEntity);
                return updateProductionCategoryDto;
            }
            return null;
        }
    }
}
