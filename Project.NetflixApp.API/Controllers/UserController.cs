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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var userResponse = await _userService.GetAllAsync();
            if(userResponse.ResponseType == ResponseType.Success)
            {
                return Ok(userResponse.Data);
            }
            return BadRequest();
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var userResponse = await _userService.GetByIdAsync(id);
            if(userResponse.ResponseType == ResponseType.NotFound)
            {
                return NotFound(userResponse.Message);
            }
            return Ok(userResponse.Data);
        }
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
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertAsync(CreateUserDto createUserDto)
        {
            var insertResponse = await _userService.InsertAsync(createUserDto);
            if(insertResponse.ResponseType == ResponseType.ValidationError)
            {
                return BadRequest(insertResponse.CustomValidationErrors);
            }
            return Ok(insertResponse.Message);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateAsync(UpdateUserDto updateUserDto)
        {
            var updateResponse = await _userService.UpdateAsync(updateUserDto);
            if(updateResponse.ResponseType == ResponseType.NotFound)
            {
                return NotFound(updateResponse.Message);
            }
            else if(updateResponse.ResponseType == ResponseType.ValidationError)
            {
                return BadRequest(updateResponse.CustomValidationErrors);
            }
            return Ok(updateResponse.Message);
        }
    }
}
