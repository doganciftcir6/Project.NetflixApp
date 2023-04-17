﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Dtos.UserDtos;
using System.Threading.Tasks;

namespace Project.NetflixApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterAsync(RegisterUserDto registerUserDto)
        {
            var registerResponse = await _authService.RegisterAsync(registerUserDto);
            if(registerResponse.ResponseType == ResponseType.ValidationError)
            {
                return BadRequest(registerResponse.CustomValidationErrors);
            }
            else if(registerResponse.ResponseType == ResponseType.Error)
            {
                //hata mesajları birleşik geliyor onları birbirinden ayıralım. ^ ifadesi silinip sonrası ayrıalcaktır.
                var errorMessages = registerResponse.Message.Split('^');
                return BadRequest(errorMessages);
            }
            return Ok(registerResponse.Message);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> LoginAsync(LoginUserDto loginUserDto)
        {
            var loginResponse = await _authService.LoginAsync(loginUserDto);
            if(loginResponse.ResponseType == ResponseType.Error)
            {
                return NotFound(loginResponse.Message);
            }
            else if(loginResponse.ResponseType == ResponseType.ValidationError)
            {
                return BadRequest(loginResponse.CustomValidationErrors);
            }
            return Ok(loginResponse.Message);
        }
    }
}