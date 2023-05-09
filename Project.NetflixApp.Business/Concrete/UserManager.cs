using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Business.Extensions;
using Project.NetflixApp.Business.Helpers.Constans;
using Project.NetflixApp.Business.Helpers.UserUploadHelpers;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Common.Utilities.ErrorsEngine;
using Project.NetflixApp.Common.Utilities.Hashing;
using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Common.Utilities.Results.Concrete;
using Project.NetflixApp.DataAccess.Repositories.Abstract;
using Project.NetflixApp.Dtos.UserDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateUserDto> _createUserDtoValidator;
        private readonly IValidator<UpdateUserDto> _updateUserDtoValidator;
        private readonly IValidator<RegisterUserDto> _registerUserDtoValidator;
        private readonly IValidator<LoginUserDto> _loginUserDtoValidator;
        private readonly IHostingEnvironment _hostingEnvironment;

        public UserManager(IUserRepository userRepository, IMapper mapper, IValidator<CreateUserDto> createUserDtoValidator, IValidator<UpdateUserDto> updateUserDtoValidator, IValidator<RegisterUserDto> registerUserDtoValidator, IValidator<LoginUserDto> loginUserDtoValidator, IUserOperationClaimRepository userOperationClaimRepository, IHostingEnvironment hostingEnvironment)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _createUserDtoValidator = createUserDtoValidator;
            _updateUserDtoValidator = updateUserDtoValidator;
            _registerUserDtoValidator = registerUserDtoValidator;
            _loginUserDtoValidator = loginUserDtoValidator;
            _userOperationClaimRepository = userOperationClaimRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IResponse> DeleteAsync(int id)
        {
            var dtoData = await GetByIdAsync(id);

            var data = await _userRepository.GetByIdAsync(id);
            if (data != null)
            {
                await _userRepository.DeleteAsync(data);
                //silme işlemi sonrasında kişinin upload dosyasınıda serverdan silelim.
                if (dtoData != null)
                {
                    var helperClass = new FileRemoveFromServerHelper(_hostingEnvironment);
                    helperClass.DeleteFileRun(dtoData.Data.ImageUrl);
                }
                return new Response(ResponseType.Success, UserMessages.Deleted);
            }
            return new Response(ResponseType.NotFound, UserMessages.NotDeleted);
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
            return new DataResponse<GetUserWithoutPasswordDto>(ResponseType.NotFound, $"{UserMessages.NotFound}" + $"{id}");
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
            return new DataResponse<GetUserWithoutPasswordDto>(ResponseType.NotFound, $"{UserMessages.NotFound}" + $"{id}");
        }
        public async Task<IDataResponse<GetUserDto>> GetByEmailAsync(string email)
        {
            var entityData = await _userRepository.GetByFilterAsync(x => x.Email == email);
            if (entityData != null)
            {
                var mappingDto = _mapper.Map<GetUserDto>(entityData);
                return new DataResponse<GetUserDto>(ResponseType.Success, mappingDto);
            }
            return new DataResponse<GetUserDto>(ResponseType.NotFound, UserMessages.NotFoundEmail);
        }
        public async Task<List<OperationClaim>> GetUserOperationClaims(int userId)
        {
            var query = _userOperationClaimRepository.GetQuery();
            var data = await query.Include(x => x.OperationClaim).Where(x => x.UserId == userId).Select(x => x.OperationClaim).ToListAsync();
            if (data != null)
            {
                return data;
            }
            return null;
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
        public async Task CreateUserAsync(RegisterUserDto registerUserDto, int roleId)
        {
            byte[] passwordHash, passwordSalt;
            //hashleme işlemi burda yapılsın
            //değişmiş veriyi burda out ile tekrar yakalıyoruz. bu sayede void metotta değişmiş verileri geri yakalayarak mapleme yapabiliyorum.
            HashingHelper.CreatePassword(registerUserDto.Password, out passwordHash, out passwordSalt);

            var mappingEntity = _mapper.Map<User>(registerUserDto);
            //upload sql tablo ataması
            if (registerUserDto.ImageUrl != null)
            {
                var createSqlName = Path.GetFileNameWithoutExtension(registerUserDto.ImageUrl.FileName) + DateTime.UtcNow.Minute + DateTime.UtcNow.Second + Path.GetExtension(registerUserDto.ImageUrl.FileName);
                mappingEntity.ImageUrl = createSqlName;
            }
            mappingEntity.PasswordHash = passwordHash;
            mappingEntity.PasswordSalt = passwordSalt;
            await _userRepository.InsertAsync(mappingEntity);
            await _userOperationClaimRepository.InsertAsync(new UserOperationClaim()
            {
                User = mappingEntity,
                OperationClaimId = roleId
            });
        }

        public async Task<IResponse> UpdateAsync(UpdateUserDto updateUserDto, IFormFile image)
        {
            //update işlemi sırasında kullanıcı şifrelerle ilgilenmeyecek.
            var oldData = await _userRepository.AsNoTrackingGetByFilterAsync(x => x.Id == updateUserDto.Id);
            if (oldData != null)
            {
                var validationResponse = _updateUserDtoValidator.Validate(updateUserDto);
                if (validationResponse.IsValid)
                {
                    if (image != null)
                    {
                        //upload işlemleri
                        IResponse uploadCheck = ErrorsEngineHelper.Run
                        (
                             UserUploadSecurityHelper.CheckIfImageExtensionsAllow(image.FileName),
                             UserUploadSecurityHelper.CheckImageNameDot(image),
                             UserUploadSecurityHelper.CheckImageName(image.FileName),
                             UserUploadSecurityHelper.CheckIfImageSizeIsLessThanOneMb(image.Length)
                        );
                        if (uploadCheck.ResponseType == ResponseType.Success)
                        {
                            //upload
                            await UploadUserHelper.CreateInstance(_hostingEnvironment).Upload(image);
                            var createSqlName = Path.GetFileNameWithoutExtension(image.FileName) + DateTime.UtcNow.Minute + DateTime.UtcNow.Second + Path.GetExtension(image.FileName);
                            oldData.ImageUrl = createSqlName;
                        }
                        else
                        {
                            return uploadCheck;
                        }
                    }
                    var mappingEntity = _mapper.Map<User>(updateUserDto);
                    mappingEntity.ImageUrl = oldData.ImageUrl;
                    mappingEntity.PasswordSalt = oldData.PasswordSalt;
                    mappingEntity.PasswordHash = oldData.PasswordHash;
                    await _userRepository.UpdateAsync(mappingEntity);
                    return new Response(ResponseType.Success, UserMessages.Updated);

                }
                return new Response(ResponseType.ValidationError, validationResponse.ConvertToCustomValidationError());
            }
            return new Response(ResponseType.NotFound, $"{UserMessages.NotUpdated}" + $"{oldData.Id}");
        }
    }
}
