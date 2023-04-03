using AutoMapper;
using FluentValidation;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.DataAccess.Repositories.Abstract;
using Project.NetflixApp.Dtos.UserOperationClaimDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateUserOperationClaimDto> _createUserOperationClaimDtoValidator;
        private readonly IValidator<UpdateUserOperationClaimDto> _updateUserOperationClaimDtoValidator;

        public UserOperationClaimManager(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, IValidator<CreateUserOperationClaimDto> createUserOperationClaimDtoValidator, IValidator<UpdateUserOperationClaimDto> updateUserOperationClaimDtoValidator)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _mapper = mapper;
            _createUserOperationClaimDtoValidator = createUserOperationClaimDtoValidator;
            _updateUserOperationClaimDtoValidator = updateUserOperationClaimDtoValidator;
        }

        public async Task DeleteAsync(int id)
        {
            var data = await _userOperationClaimRepository.GetByIdAsync(id);
            if(data != null)
            {
                await _userOperationClaimRepository.DeleteAsync(data);
            }
        }

        public async Task<IEnumerable<GetUserOperationClaimDto>> GetAllAsync()
        {
            var entityData = await _userOperationClaimRepository.GetAllAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetUserOperationClaimDto>>(entityData);
            return mappingDto;
        }

        public async Task<GetUserOperationClaimDto> GetByIdAsync(int id)
        {
            var entityData = await _userOperationClaimRepository.GetByFilterAsync(x => x.Id == id);
            if(entityData != null)
            {
                var mappingDto = _mapper.Map<GetUserOperationClaimDto>(entityData);
                return mappingDto;
            }
            return null;
        }

        public async Task<CreateUserOperationClaimDto> InsertAsync(CreateUserOperationClaimDto createUserOperationClaimDto)
        {
            var validationResponse = _createUserOperationClaimDtoValidator.Validate(createUserOperationClaimDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<UserOperationClaim>(createUserOperationClaimDto);
                await _userOperationClaimRepository.InsertAsync(mappingEntity);
                return createUserOperationClaimDto;
            }
            return null;
        }

        public async Task<UpdateUserOperationClaimDto> UpdateAsync(UpdateUserOperationClaimDto updateUserOperationClaimDto)
        {
            var validationResponse = _updateUserOperationClaimDtoValidator.Validate(updateUserOperationClaimDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<UserOperationClaim>(updateUserOperationClaimDto);
                await _userOperationClaimRepository.UpdateAsync(mappingEntity);
                return updateUserOperationClaimDto;
            }
            return null;
        }
    }
}
