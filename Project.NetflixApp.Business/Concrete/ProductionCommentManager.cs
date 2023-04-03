using AutoMapper;
using FluentValidation;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.DataAccess.Repositories.Abstract;
using Project.NetflixApp.Dtos.ProductionCommentDtos;
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

        public async Task DeleteAsync(int id)
        {
            var data = await _productionCommentRepository.GetByIdAsync(id);
            if(data != null)
            {
                await _productionCommentRepository.DeleteAsync(data);
            }
        }

        public async Task<IEnumerable<GetProductionCommentDto>> GetAllAsync()
        {
            var entityData = await _productionCommentRepository.GetAllAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetProductionCommentDto>>(entityData);
            return mappingDto;
        }

        public async Task<GetProductionCommentDto> GetByIdAsync(int id)
        {
            var entityData = await _productionCommentRepository.GetByFilterAsync(x => x.Id == id);
            if(entityData != null)
            {
                var mappingDto = _mapper.Map<GetProductionCommentDto>(entityData);
                return mappingDto;
            }
            return null;
        }

        public async Task<CreateProductionCommentDto> InsertAsync(CreateProductionCommentDto createProductionCommentDto)
        {
            var validationResponse = _createProductionCommentDtoValidator.Validate(createProductionCommentDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<ProductionComment>(createProductionCommentDto);
                await _productionCommentRepository.InsertAsync(mappingEntity);
                return createProductionCommentDto;
            }
            return null;
        }

        public async Task<UpdateProductionCommentDto> UpdateAsync(UpdateProductionCommentDto updateProductionCommentDto)
        {
            var validationResponse = _updateProductionCommentDtoValidator.Validate(updateProductionCommentDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<ProductionComment>(updateProductionCommentDto);
                await _productionCommentRepository.UpdateAsync(mappingEntity);
                return updateProductionCommentDto;
            }
            return null;
        }
    }
}
