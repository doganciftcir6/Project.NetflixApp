using Microsoft.AspNetCore.Http;
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
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterAsync(RegisterUserDto registerUserDto)
        {
            var registerResponse = await _userService.RegisterAsync(registerUserDto);
            if(registerResponse.ResponseType == ResponseType.ValidationError)
            {
                return BadRequest(registerResponse.CustomValidationErrors);
            }
            return Ok(registerResponse.Message);
        }
    }
}
