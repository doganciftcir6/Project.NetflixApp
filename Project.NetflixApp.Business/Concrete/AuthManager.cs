using FluentValidation;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Business.Extensions;
using Project.NetflixApp.Business.Helpers;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Common.Utilities.ErrorsEngine;
using Project.NetflixApp.Common.Utilities.Hashing;
using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Common.Utilities.Results.Concrete;
using Project.NetflixApp.Dtos.UserDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IValidator<RegisterUserDto> _registerUserDtoValidator;
        private readonly IValidator<LoginUserDto> _loginUserDtoValidator;
        public AuthManager(IUserService userService, IValidator<RegisterUserDto> registerUserDtoValidator, IValidator<LoginUserDto> loginUserDtoValidator)
        {
            _userService = userService;
            _registerUserDtoValidator = registerUserDtoValidator;
            _loginUserDtoValidator = loginUserDtoValidator;
        }

        public async Task<IResponse> LoginAsync(LoginUserDto loginUserDto)
        {
            var validationResponse = _loginUserDtoValidator.Validate(loginUserDto);
            if (validationResponse.IsValid)
            {
                var userResponse = await _userService.GetByEmailAsync(loginUserDto.Email);
                if (userResponse.ResponseType == ResponseType.Success)
                {
                    //email uyuyor şifre kontrolü yapalım.
                    var passwordResponse = HashingHelper.VerifyPasswordHash(loginUserDto.Password, userResponse.Data.PasswordHash, userResponse.Data.PasswordSalt);
                    if (passwordResponse.ResponseType == ResponseType.Success)
                    {
                        return new Response(ResponseType.Success, "User login success.");
                    }
                    //şifre uymuyor
                    return new Response(ResponseType.Error, "Incorrect password entered.");
                }
                //email uymuyor
                return new Response(ResponseType.Error, "Incorrect email entered");
            }
            return new Response(ResponseType.ValidationError, validationResponse.ConvertToCustomValidationError());
        }

        public async Task<IResponse> RegisterAsync(RegisterUserDto registerUserDto)
        {
            var validationResponse = _registerUserDtoValidator.Validate(registerUserDto);
            if (validationResponse.IsValid)
            {
                IResponse userInfChecks = ErrorsEngineHelper.Run
                (
                   await RegisterRuleHelper.CreateInstance(_userService).CheckEmailExists(registerUserDto.Email)
                );
                if (userInfChecks.ResponseType == ResponseType.Success)
                {
                    await _userService.CreateUserAsync(registerUserDto);
                    return new Response(ResponseType.Success, "The user adding process has been successfully completed.");
                }
                else
                {
                    return userInfChecks;
                }
            }
            return new Response(ResponseType.ValidationError, validationResponse.ConvertToCustomValidationError());
        }
    }
}
