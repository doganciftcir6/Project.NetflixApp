using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Dtos.DuractionDtos;
using System.Threading.Tasks;

namespace Project.NetflixApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DuractionController : ControllerBase
    {
        private readonly IDuractionService _duractionService;
        public DuractionController(IDuractionService duractionService)
        {
            _duractionService = duractionService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var duractionResponse = await _duractionService.GetAllAsync();
            if(duractionResponse.ResponseType == ResponseType.Success)
            {
                return Ok(duractionResponse.Data);
            }
            return BadRequest();
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var duractionResponse = await _duractionService.GetByIdAsync(id);
            if(duractionResponse.ResponseType == ResponseType.NotFound)
            {
                return NotFound(duractionResponse.Message);
            }
            return Ok(duractionResponse.Data);
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleteResponse = await _duractionService.DeleteAsync(id);
            if(deleteResponse.ResponseType == ResponseType.NotFound)
            {
                return NotFound(deleteResponse.Message);
            }
            return Ok(deleteResponse.Message);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertAsync(CreateDuractionDto createDuractionDto)
        {
            var insertResponse = await _duractionService.InsertAsync(createDuractionDto);
            if(insertResponse.ResponseType == ResponseType.ValidationError)
            {
                return BadRequest(insertResponse.CustomValidationErrors);
            }
            return Ok(insertResponse.Message);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateAsync(UpdateDuractionDto updateDuractionDto)
        {
            var updateResponse = await _duractionService.UpdateAsync(updateDuractionDto);
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
