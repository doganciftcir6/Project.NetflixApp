using AutoMapper;
using FluentValidation;
using Project.NetflixApp.Business.Abstract;
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

        public async Task DeleteAsync(int id)
        {
            var data = await _operationClaimRepository.GetByIdAsync(id);
            if(data != null)
            {
                await _operationClaimRepository.DeleteAsync(data);
            }
        }

        public async Task<IEnumerable<GetOperationClaimDto>> GetAllAsync()
        {
            var entityData = await _operationClaimRepository.GetAllAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetOperationClaimDto>>(entityData);
            return mappingDto;
        }

        public async Task<GetOperationClaimDto> GetByIdAsync(int id)
        {
            var entityData = await _operationClaimRepository.GetByFilterAsync(x => x.Id == id);
            if(entityData != null)
            {
                var mappingDto = _mapper.Map<GetOperationClaimDto>(entityData);
                return mappingDto;
            }
            return null;
        }

        public async Task<CreateOperationClaimDto> InsertAsync(CreateOperationClaimDto operationClaimDto)
        {
            var validationResponse = _createOperationClaimDtoValidator.Validate(operationClaimDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<OperationClaim>(operationClaimDto);
                await _operationClaimRepository.InsertAsync(mappingEntity);
                return operationClaimDto;
            }
            return null;
        }

        public async Task<UpdateOperationClaimDto> UpdateAsync(UpdateOperationClaimDto operationClaimDto)
        {
            var validationResponse = _updateOperationClaimDtoValidator.Validate(operationClaimDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<OperationClaim>(operationClaimDto);
                await _operationClaimRepository.UpdateAsync(mappingEntity);
                return operationClaimDto;
            }
            return null;
        }
    }
}
