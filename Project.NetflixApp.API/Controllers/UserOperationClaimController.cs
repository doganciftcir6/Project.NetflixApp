using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Dtos.UserOperationClaimDtos;
using System.Threading.Tasks;

namespace Project.NetflixApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimController : ControllerBase
    {
        private readonly IUserOperationClaimService _userOperationClaimService;
        public UserOperationClaimController(IUserOperationClaimService userOperationClaimService)
        {
            _userOperationClaimService = userOperationClaimService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var userOperationClaimResponse = await _userOperationClaimService.GetAllWithRelationsAsync();
            if(userOperationClaimResponse.ResponseType == ResponseType.Success)
            {
                return Ok(userOperationClaimResponse.Data);
            }
            return BadRequest();
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var userOperationClaimResponse = await _userOperationClaimService.GetByIdWithRelationsAsync(id);
            if(userOperationClaimResponse.ResponseType == ResponseType.NotFound)
            {
                return NotFound(userOperationClaimResponse.Message);
            }
            return Ok(userOperationClaimResponse.Data);
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleteResponse = await _userOperationClaimService.DeleteAsync(id);
            if(deleteResponse.ResponseType == ResponseType.NotFound)
            {
                return NotFound(deleteResponse.Message);
            }
            return Ok(deleteResponse.Message);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertAsync(CreateUserOperationClaimDto createUserOperationClaimDto)
        {
            var insertResponse = await _userOperationClaimService.InsertAsync(createUserOperationClaimDto);
            if(insertResponse.ResponseType == ResponseType.ValidationError)
            {
                return BadRequest(insertResponse.CustomValidationErrors);
            }
            return Ok(insertResponse.Message);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateAsync(UpdateUserOperationClaimDto updateUserOperationClaimDto)
        {
            var updateResponse = await _userOperationClaimService.UpdateAsync(updateUserOperationClaimDto);
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
