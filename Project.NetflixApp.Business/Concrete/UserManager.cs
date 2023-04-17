using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Business.Extensions;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Common.Utilities.Hashing;
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
        private readonly IValidator<RegisterUserDto> _registerUserDtoValidator;
        private readonly IValidator<LoginUserDto> _loginUserDtoValidator;

        public UserManager(IUserRepository userRepository, IMapper mapper, IValidator<CreateUserDto> createUserDtoValidator, IValidator<UpdateUserDto> updateUserDtoValidator, IValidator<RegisterUserDto> registerUserDtoValidator, IValidator<LoginUserDto> loginUserDtoValidator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _createUserDtoValidator = createUserDtoValidator;
            _updateUserDtoValidator = updateUserDtoValidator;
            _registerUserDtoValidator = registerUserDtoValidator;
            _loginUserDtoValidator = loginUserDtoValidator;
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

        public async Task<IDataResponse<IEnumerable<GetUserWithoutPasswordDto>>> GetAllAsync()
        {
            var entityData = await _userRepository.GetAllAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetUserWithoutPasswordDto>>(entityData);
            return new DataResponse<IEnumerable<GetUserWithoutPasswordDto>>(ResponseType.Success, mappingDto);
        }
        public async Task<IDataResponse<IEnumerable<GetUserWithoutPasswordDto>>> GetAllWithGenderAsync()
        {
            var query = _userRepository.GetQuery();
            var entityData = await query.AsNoTracking().Include(x => x.Gender).ToListAsync();
            var mappingDto = _mapper.Map<IEnumerable<GetUserWithoutPasswordDto>>(entityData);
            return new DataResponse<IEnumerable<GetUserWithoutPasswordDto>>(ResponseType.Success, mappingDto);
        }

        public async Task<IDataResponse<GetUserWithoutPasswordDto>> GetByIdAsync(int id)
        {
            var entityData = await _userRepository.GetByFilterAsync(x => x.Id == id);
            if (entityData != null)
            {
                var mappingDto = _mapper.Map<GetUserWithoutPasswordDto>(entityData);
                return new DataResponse<GetUserWithoutPasswordDto>(ResponseType.Success, mappingDto);
            }
            return new DataResponse<GetUserWithoutPasswordDto>(ResponseType.NotFound, $"The related user could not be found. User Id:");
        }
        public async Task<IDataResponse<GetUserWithoutPasswordDto>> GetByIdWithGenderAsync(int id)
        {
            var query = _userRepository.GetQuery();
            var entityData = await query.Where(x => x.Id == id).Include(x => x.Gender).FirstOrDefaultAsync();
            if (entityData != null)
            {
                var mappingDto = _mapper.Map<GetUserWithoutPasswordDto>(entityData);
                return new DataResponse<GetUserWithoutPasswordDto>(ResponseType.Success, mappingDto);
            }
            return new DataResponse<GetUserWithoutPasswordDto>(ResponseType.NotFound, $"The related user could not be found. User Id:");
        }
        public async Task<IDataResponse<GetUserDto>> GetByEmailAsync(string email)
        {
            var entityData = await _userRepository.GetByFilterAsync(x => x.Email == email);
            if (entityData != null)
            {
                var mappingDto = _mapper.Map<GetUserDto>(entityData);
                return new DataResponse<GetUserDto>(ResponseType.Success, mappingDto);
            }
            return new DataResponse<GetUserDto>(ResponseType.NotFound, "No user with the related email address could be found.");
        }
        public async Task<IResponse> UserEmailExistAsync(string email)
        {
            var entityData = await _userRepository.GetByFilterAsync(x => x.Email == email);
            if (entityData != null)
            {
                return new Response(ResponseType.Error);
            }
            return new Response(ResponseType.Success);
        }
        public async Task CreateUserAsync(RegisterUserDto registerUserDto)
        {
            byte[] passwordHash, passwordSalt;
            //hashleme işlemi burda yapılsın
            //değişmiş veriyi burda out ile tekrar yakalıyoruz. bu sayede void metotta değişmiş verileri geri yakalayarak mapleme yapabiliyorum.
            HashingHelper.CreatePassword(registerUserDto.Password, out passwordHash, out passwordSalt);

            var mappingEntity = _mapper.Map<User>(registerUserDto);
            mappingEntity.PasswordHash = passwordHash;
            mappingEntity.PasswordSalt = passwordSalt;
            await _userRepository.InsertAsync(mappingEntity);
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
