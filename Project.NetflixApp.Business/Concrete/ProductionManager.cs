using AutoMapper;
using FluentValidation;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.DataAccess.Repositories.Abstract;
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

        public async Task DeleteAsync(int id)
        {
            var data = await _productionRepository.GetByIdAsync(id);
            if(data != null)
            {
                await _productionRepository.DeleteAsync(data);
            }
        }

        public async Task<IEnumerable<GetProductionDto>> GetAllAsync()
        {
            var entityData = await _productionRepository.GetAllAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetProductionDto>>(entityData);
            return mappingDto;
        }

        public async Task<GetProductionDto> GetByIdAsync(int id)
        {
            var entityData = await _productionRepository.GetByFilterAsync(x => x.Id == id);
            if(entityData != null)
            {
                var mappingDto = _mapper.Map<GetProductionDto>(entityData);
                return mappingDto;
            }
            return null;
        }

        public async Task<CreateProductionDto> InsertAsync(CreateProductionDto createProductionDto)
        {
            var validationResponse = _createProductionDtoValidator.Validate(createProductionDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<Production>(createProductionDto);
                await _productionRepository.InsertAsync(mappingEntity);
                return createProductionDto;
            }
            return null;
        }

        public async Task<UpdateProductionDto> UpdateAsync(UpdateProductionDto updateProductionDto)
        {
            var validationResponse = _updateProductionDtoValidator.Validate(updateProductionDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<Production>(updateProductionDto);
                await _productionRepository.UpdateAsync(mappingEntity);
                return updateProductionDto;
            }
            return null;
        }
    }
}
