using AutoMapper;
using FluentValidation;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Business.Extensions;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Common.Utilities.Results.Concrete;
using Project.NetflixApp.DataAccess.Repositories.Abstract;
using Project.NetflixApp.Dtos.UserDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateUserDto> _createUserDtoValidator;
        private readonly IValidator<UpdateUserDto> _updateUserDtoValidator;

        public UserManager(IUserRepository userRepository, IMapper mapper, IValidator<CreateUserDto> createUserDtoValidator, IValidator<UpdateUserDto> updateUserDtoValidator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _createUserDtoValidator = createUserDtoValidator;
            _updateUserDtoValidator = updateUserDtoValidator;
        }

        public async Task<IResponse> DeleteAsync(int id)
        {
            var data = await _userRepository.GetByIdAsync(id);
            if (data != null)
            {
                await _userRepository.DeleteAsync(data);
                return new Response(ResponseType.Success, "The user was successfully deleted");
            }
            return new Response(ResponseType.NotFound, "The user parameter could not be deleted because the user could not be found.");
        }

        public async Task<IDataResponse<IEnumerable<GetUserDto>>> GetAllAsync()
        {
            var entityData = await _userRepository.GetAllAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetUserDto>>(entityData);
            return new DataResponse<IEnumerable<GetUserDto>>(ResponseType.Success, mappingDto);
        }

        public async Task<IDataResponse<GetUserDto>> GetByIdAsync(int id)
        {
            var entityData = await _userRepository.GetByFilterAsync(x => x.Id == id);
            if (entityData != null)
            {
                var mappingDto = _mapper.Map<GetUserDto>(entityData);
                return new DataResponse<GetUserDto>(ResponseType.Success, mappingDto);
            }
            return new DataResponse<GetUserDto>(ResponseType.NotFound, $"The related user could not be found. User Id:");
        }

        public async Task<IResponse> InsertAsync(CreateUserDto createUserDto)
        {
            var validationResponse = _createUserDtoValidator.Validate(createUserDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<User>(createUserDto);
                await _userRepository.InsertAsync(mappingEntity);
                return new Response(ResponseType.Success, "The user adding process has been successfully completed.");
            }
            return new Response(ResponseType.ValidationError, validationResponse.ConvertToCustomValidationError());
        }

        public async Task<IResponse> UpdateAsync(UpdateUserDto updateUserDto)
        {
            var oldData = await _userRepository.AsNoTrackingGetByFilterAsync(x => x.Id == updateUserDto.Id);
            if (oldData != null)
            {
                var validationResponse = _updateUserDtoValidator.Validate(updateUserDto);
                if (validationResponse.IsValid)
                {
                    var mappingEntity = _mapper.Map<User>(updateUserDto);
                    await _userRepository.UpdateAsync(mappingEntity);
                    return new Response(ResponseType.Success, "The user updating process has been successfully completed.");
                }
                return new Response(ResponseType.ValidationError, validationResponse.ConvertToCustomValidationError());
            }
            return new Response(ResponseType.NotFound, "The related user could not be found. So the update process could not be completed. User Id:");
        }
    }
}
