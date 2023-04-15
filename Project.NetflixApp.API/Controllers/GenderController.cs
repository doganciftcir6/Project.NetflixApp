using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Dtos.GenderDtos;
using System.Threading.Tasks;

namespace Project.NetflixApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly IGenderService _genderService;
        public GenderController(IGenderService genderService)
        {
            _genderService = genderService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var genderResponse = await _genderService.GetAllAsync();
            if(genderResponse.ResponseType == ResponseType.Success)
            {
                return Ok(genderResponse.Data);
            }
            return BadRequest();
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var genderResponse = await _genderService.GetByIdAsync(id);
            if(genderResponse.ResponseType == ResponseType.NotFound)
            {
                return NotFound(genderResponse.Message);
            }
            return Ok(genderResponse.Data);
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleteResponse = await _genderService.DeleteAsync(id);
            if(deleteResponse.ResponseType == ResponseType.NotFound)
            {
                return NotFound(deleteResponse.Message);
            }
            return Ok(deleteResponse.Message);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertAsync(CreateGenderDto createGenderDto)
        {
            var insertResponse = await _genderService.InsertAsync(createGenderDto);
            if(insertResponse.ResponseType == ResponseType.ValidationError)
            {
                return BadRequest(insertResponse.CustomValidationErrors);
            }
            return Ok(insertResponse.Message);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateAsync(UpdateGenderDto updateGenderDto)
        {
            var updateResponse = await _genderService.UpdateAsync(updateGenderDto);
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
