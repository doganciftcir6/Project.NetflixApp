using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Dtos.UserDtos;
using System.Linq;
using System.Threading.Tasks;

namespace Project.NetflixApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var userResponse = await _userService.GetAllWithGenderAsync();
            if(userResponse.ResponseType == ResponseType.Success)
            {
                return Ok(userResponse.Data);
            }
            return BadRequest();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var userResponse = await _userService.GetByIdWithGenderAsync(id);
            if(userResponse.ResponseType == ResponseType.NotFound)
            {
                return NotFound(userResponse.Message);
            }
            return Ok(userResponse.Data);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleteResponse = await _userService.DeleteAsync(id);
            if(deleteResponse.ResponseType == ResponseType.NotFound)
            {
                return NotFound(deleteResponse.Message);
            }
            return Ok(deleteResponse.Message);
        }
        [Authorize(Roles = "Admin,Member")]
        [HttpPost("[action]")]
        public async Task<IActionResult> ChangePasswordAsync(UserChangePasswordDto userChangePasswordDto)
        {
            var changePasswordResponse = await _userService.ChangePasswordAsync(userChangePasswordDto);
            if (changePasswordResponse.ResponseType == ResponseType.Error)
            {
                return BadRequest(changePasswordResponse.Message);
            }
            else if (changePasswordResponse.ResponseType == ResponseType.NotFound)
            {
                return NotFound(changePasswordResponse.Message);
            }
            else if(changePasswordResponse.ResponseType == ResponseType.ValidationError)
            {
                return BadRequest(changePasswordResponse.CustomValidationErrors);
            }
            return Ok(changePasswordResponse.Message);
        }
        [Authorize(Roles = "Admin,Member")]
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateAsync([FromForm]UpdateUserDto updateUserDto, IFormFile image)
        {
            var updateResponse = await _userService.UpdateAsync(updateUserDto, image);
            if(updateResponse.ResponseType == ResponseType.NotFound)
            {
                return NotFound(updateResponse.Message);
            }
            else if(updateResponse.ResponseType == ResponseType.Error)
            {
                //hata mesajları birleşik geliyor onları birbirinden ayıralım. ^ ifadesi silinip sonrası ayrıalcaktır.
                //mesajların sonunda ek boş bir mesaj geliyordu ondanda where sorgusuyla kurtulalım bu noktada.
                var errorMessages = updateResponse.Message.Split('^').Where(x => !string.IsNullOrEmpty(x));
                return BadRequest(errorMessages);
            }
            else if(updateResponse.ResponseType == ResponseType.ValidationError)
            {
                return BadRequest(updateResponse.CustomValidationErrors);
            }
            return Ok(updateResponse.Message);
        }
    }
}
