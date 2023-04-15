using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Dtos.TypeEntityDtos;
using System.Threading.Tasks;

namespace Project.NetflixApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeEntityController : ControllerBase
    {
        private readonly ITypeEntityService _typeEntityService;
        public TypeEntityController(ITypeEntityService typeEntityService)
        {
            _typeEntityService = typeEntityService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var typeEntityResponse = await _typeEntityService.GetAllAsync();
            if(typeEntityResponse.ResponseType == ResponseType.Success)
            {
                return Ok(typeEntityResponse.Data);
            }
            return BadRequest();
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var typeEntityResponse = await _typeEntityService.GetByIdAsync(id);
            if(typeEntityResponse.ResponseType == ResponseType.NotFound)
            {
                return NotFound(typeEntityResponse.Message);
            }
            return Ok(typeEntityResponse.Data);
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleteResponse = await _typeEntityService.DeleteAsync(id);
            if(deleteResponse.ResponseType == ResponseType.NotFound)
            {
                return NotFound(deleteResponse.Message);
            }
            return Ok(deleteResponse.Message);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertAsync(CreateTypeEntityDto createTypeEntityDto)
        {
            var insertResponse = await _typeEntityService.InsertAsync(createTypeEntityDto);
            if(insertResponse.ResponseType == ResponseType.ValidationError)
            {
                return BadRequest(insertResponse.CustomValidationErrors);
            }
            return Ok(insertResponse.Message);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateAsync(UpdateTypeEntityDto updateTypeEntityDto)
        {
            var updateResponse = await _typeEntityService.UpdateAsync(updateTypeEntityDto);
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
