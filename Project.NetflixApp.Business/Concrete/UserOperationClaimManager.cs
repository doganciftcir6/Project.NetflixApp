using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Business.Extensions;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Common.Utilities.Results.Concrete;
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

        public async Task<IResponse> DeleteAsync(int id)
        {
            var data = await _userOperationClaimRepository.GetByIdAsync(id);
            if (data != null)
            {
                await _userOperationClaimRepository.DeleteAsync(data);
                return new Response(ResponseType.Success, "The useroperationclaim was successfully deleted");
            }
            return new Response(ResponseType.NotFound, "The useroperationclaim parameter could not be deleted because the useroperationclaim could not be found.");
        }

        public async Task<IDataResponse<IEnumerable<GetUserOperationClaimDto>>> GetAllAsync()
        {
            var entityData = await _userOperationClaimRepository.GetAllAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetUserOperationClaimDto>>(entityData);
            return new DataResponse<IEnumerable<GetUserOperationClaimDto>>(ResponseType.Success, mappingDto);
        }

        public async Task<IDataResponse<IEnumerable<GetUserOperationClaimDto>>> GetAllWithRelationsAsync()
        {
            var query = _userOperationClaimRepository.GetQuery();
            var entityData = await query.AsNoTracking().Include(x => x.User).ThenInclude(x => x.Gender).Include(x => x.OperationClaim).ToListAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetUserOperationClaimDto>>(entityData);
            return new DataResponse<IEnumerable<GetUserOperationClaimDto>>(ResponseType.Success, mappingDto);
        }

        public async Task<IDataResponse<GetUserOperationClaimDto>> GetByIdAsync(int id)
        {
            var entityData = await _userOperationClaimRepository.GetByFilterAsync(x => x.Id == id);
            if (entityData != null)
            {
                var mappingDto = _mapper.Map<GetUserOperationClaimDto>(entityData);
                return new DataResponse<GetUserOperationClaimDto>(ResponseType.Success, mappingDto);
            }
            return new DataResponse<GetUserOperationClaimDto>(ResponseType.NotFound, $"The related useroperationclaim could not be found. Useroperationclaim Id:");
        }

        public async Task<IDataResponse<GetUserOperationClaimDto>> GetByIdWithRelationsAsync(int id)
        {
            var query = _userOperationClaimRepository.GetQuery();
            var entityData = await query.Where(x => x.Id == id).Include(x => x.User).ThenInclude(x => x.Gender).Include(x => x.OperationClaim).FirstOrDefaultAsync();
            if (entityData != null)
            {
                var mappingDto = _mapper.Map<GetUserOperationClaimDto>(entityData);
                return new DataResponse<GetUserOperationClaimDto>(ResponseType.Success, mappingDto);
            }
            return new DataResponse<GetUserOperationClaimDto>(ResponseType.NotFound, $"The related useroperationclaim could not be found. Useroperationclaim Id:");
        }

        public async Task<IResponse> InsertAsync(CreateUserOperationClaimDto createUserOperationClaimDto)
        {
            var validationResponse = _createUserOperationClaimDtoValidator.Validate(createUserOperationClaimDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<UserOperationClaim>(createUserOperationClaimDto);
                await _userOperationClaimRepository.InsertAsync(mappingEntity);
                return new Response(ResponseType.Success, "The useroperationclaim adding process has been successfully completed.");
            }
            return new Response(ResponseType.ValidationError, validationResponse.ConvertToCustomValidationError());
        }

        public async Task<IResponse> UpdateAsync(UpdateUserOperationClaimDto updateUserOperationClaimDto)
        {
            var oldData = await _userOperationClaimRepository.AsNoTrackingGetByFilterAsync(x => x.Id == updateUserOperationClaimDto.Id);
            if (oldData != null)
            {
                var validationResponse = _updateUserOperationClaimDtoValidator.Validate(updateUserOperationClaimDto);
                if (validationResponse.IsValid)
                {
                    var mappingEntity = _mapper.Map<UserOperationClaim>(updateUserOperationClaimDto);
                    await _userOperationClaimRepository.UpdateAsync(mappingEntity);
                    return new Response(ResponseType.Success, "The useroperationclaim updating process has been successfully completed.");
                }
                return new Response(ResponseType.ValidationError, validationResponse.ConvertToCustomValidationError());
            }
            return new Response(ResponseType.NotFound, "The related useroperationcliam could not be found. So the update process could not be completed. Useroperationclaim Id:");
        }
    }
}
