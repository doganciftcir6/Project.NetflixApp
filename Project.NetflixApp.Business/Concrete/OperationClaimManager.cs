using AutoMapper;
using FluentValidation;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Business.Extensions;
using Project.NetflixApp.Business.Helpers.Constans;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Common.Utilities.Results.Concrete;
using Project.NetflixApp.DataAccess.Repositories.Abstract;
using Project.NetflixApp.Dtos.OperationClaimDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOperationClaimDto> _createOperationClaimDtoValidator;
        private readonly IValidator<UpdateOperationClaimDto> _updateOperationClaimDtoValidator;

        public OperationClaimManager(IOperationClaimRepository operationClaimRepository, IMapper mapper, IValidator<CreateOperationClaimDto> createOperationClaimDtoValidator, IValidator<UpdateOperationClaimDto> updateOperationClaimDtoValidator)
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
            _createOperationClaimDtoValidator = createOperationClaimDtoValidator;
            _updateOperationClaimDtoValidator = updateOperationClaimDtoValidator;
        }

        public async Task<IResponse> DeleteAsync(int id)
        {
            var data = await _operationClaimRepository.GetByIdAsync(id);
            if (data != null)
            {
                await _operationClaimRepository.DeleteAsync(data);
                return new Response(ResponseType.Success, OperationClaimMessages.Deleted);
            }
            return new Response(ResponseType.NotFound, OperationClaimMessages.NotDeleted);
        }

        public async Task<IDataResponse<IEnumerable<GetOperationClaimDto>>> GetAllAsync()
        {
            var entityData = await _operationClaimRepository.GetAllAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetOperationClaimDto>>(entityData);
            return new DataResponse<IEnumerable<GetOperationClaimDto>>(ResponseType.Success, mappingDto);
        }

        public async Task<IDataResponse<GetOperationClaimDto>> GetByIdAsync(int id)
        {
            var entityData = await _operationClaimRepository.GetByFilterAsync(x => x.Id == id);
            if (entityData != null)
            {
                var mappingDto = _mapper.Map<GetOperationClaimDto>(entityData);
                return new DataResponse<GetOperationClaimDto>(ResponseType.Success, mappingDto);
            }
            return new DataResponse<GetOperationClaimDto>(ResponseType.NotFound, $"{OperationClaimMessages.NotFound}" + $"{id}");
        }

        public async Task<IResponse> InsertAsync(CreateOperationClaimDto operationClaimDto)
        {
            var validationResponse = _createOperationClaimDtoValidator.Validate(operationClaimDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<OperationClaim>(operationClaimDto);
                await _operationClaimRepository.InsertAsync(mappingEntity);
                return new Response(ResponseType.Success, OperationClaimMessages.Created);
            }
            return new Response(ResponseType.ValidationError, validationResponse.ConvertToCustomValidationError());
        }

        public async Task<IResponse> UpdateAsync(UpdateOperationClaimDto operationClaimDto)
        {
            var oldData = await _operationClaimRepository.AsNoTrackingGetByFilterAsync(x => x.Id == operationClaimDto.Id);
            if (oldData != null)
            {
                var validationResponse = _updateOperationClaimDtoValidator.Validate(operationClaimDto);
                if (validationResponse.IsValid)
                {
                    var mappingEntity = _mapper.Map<OperationClaim>(operationClaimDto);
                    await _operationClaimRepository.UpdateAsync(mappingEntity);
                    return new Response(ResponseType.Success, OperationClaimMessages.Updated);
                }
                return new Response(ResponseType.ValidationError, validationResponse.ConvertToCustomValidationError());
            }
            return new Response(ResponseType.NotFound, $"{OperationClaimMessages.NotUpdated}" + $"{oldData.Id}");
        }
    }
}
