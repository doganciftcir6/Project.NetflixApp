using AutoMapper;
using FluentValidation;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Business.Extensions;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Common.Utilities.Results.Concrete;
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

        public async Task<IResponse> DeleteAsync(int id)
        {
            var data = await _productionCommentRepository.GetByIdAsync(id);
            if (data != null)
            {
                await _productionCommentRepository.DeleteAsync(data);
                return new Response(ResponseType.Success, "The productioncomment was successfully deleted");
            }
            return new Response(ResponseType.NotFound, "The productioncomment parameter could not be deleted because the productioncomment could not be found.");
        }

        public async Task<IDataResponse<IEnumerable<GetProductionCommentDto>>> GetAllAsync()
        {
            var entityData = await _productionCommentRepository.GetAllAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetProductionCommentDto>>(entityData);
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
            return new DataResponse<GetProductionCommentDto>(ResponseType.NotFound, $"The related productioncomment could not be found. Productioncomment Id:");
        }

        public async Task<IResponse> InsertAsync(CreateProductionCommentDto createProductionCommentDto)
        {
            var validationResponse = _createProductionCommentDtoValidator.Validate(createProductionCommentDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<ProductionComment>(createProductionCommentDto);
                await _productionCommentRepository.InsertAsync(mappingEntity);
                return new Response(ResponseType.Success, "The productioncomment adding process has been successfully completed.");
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
                    var mappingEntity = _mapper.Map<ProductionComment>(updateProductionCommentDto);
                    await _productionCommentRepository.UpdateAsync(mappingEntity);
                    return new Response(ResponseType.Success, "The productioncomment updating process has been successfully completed.");
                }
                return new Response(ResponseType.ValidationError, validationResponse.ConvertToCustomValidationError());
            }
            return new Response(ResponseType.NotFound, "The related productioncomment could not be found. So the update process could not be completed. Productioncomment Id:");
        }
    }
}
