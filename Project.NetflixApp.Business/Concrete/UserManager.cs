using AutoMapper;
using FluentValidation;
using Project.NetflixApp.Business.Abstract;
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

        public async Task DeleteAsync(int id)
        {
            var data = await _userRepository.GetByIdAsync(id);
            if(data != null)
            {
                await _userRepository.DeleteAsync(data);
            }
        }

        public async Task<IEnumerable<GetUserDto>> GetAllAsync()
        {
            var entityData = await _userRepository.GetAllAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetUserDto>>(entityData);
            return mappingDto;
        }

        public async Task<GetUserDto> GetByIdAsync(int id)
        {
            var entityData = await _userRepository.GetByFilterAsync(x => x.Id == id);
            if(entityData != null)
            {
                var mappingDto = _mapper.Map<GetUserDto>(entityData);
                return mappingDto;
            }
            return null;
        }

        public async Task<CreateUserDto> InsertAsync(CreateUserDto createUserDto)
        {
            var validationResponse = _createUserDtoValidator.Validate(createUserDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<User>(createUserDto);
                await _userRepository.InsertAsync(mappingEntity);
                return createUserDto;
            }
            return null;
        }

        public async Task<UpdateUserDto> UpdateAsync(UpdateUserDto updateUserDto)
        {
            var validationResponse = _updateUserDtoValidator.Validate(updateUserDto);
            if (validationResponse.IsValid)
            {
                var mappingEntity = _mapper.Map<User>(updateUserDto);
                await _userRepository.UpdateAsync(mappingEntity);
                return updateUserDto;
            }
            return null;
        }
    }
}
