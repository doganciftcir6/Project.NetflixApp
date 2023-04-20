using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Dtos.ProductionDtos;
using System.Threading.Tasks;

namespace Project.NetflixApp.API.Controllers
{
    [Authorize(Roles = "Admin, Member")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionController : ControllerBase
    {
        private readonly IProductionService _productionService;
        public ProductionController(IProductionService productionService)
        {
            _productionService = productionService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var productionResponse = await _productionService.GetAllWithRelationsAsync();
            if(productionResponse.ResponseType == ResponseType.Success)
            {
                return Ok(productionResponse.Data);
            }
            return BadRequest();
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var productionResponse = await _productionService.GetByIdWithRelationsAsync(id);
            if(productionResponse.ResponseType == ResponseType.NotFound)
            {
                return NotFound(productionResponse.Message);
            }
            return Ok(productionResponse.Data);
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleteResponse = await _productionService.DeleteAsync(id);
            if(deleteResponse.ResponseType == ResponseType.NotFound)
            {
                return NotFound(deleteResponse.Message);
            }
            return Ok(deleteResponse.Message);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertAsync(CreateProductionDto createProductionDto)
        {
            var insertResponse = await _productionService.InsertAsync(createProductionDto);
            if(insertResponse.ResponseType == ResponseType.ValidationError)
            {
                return BadRequest(insertResponse.CustomValidationErrors);
            }
            return Ok(insertResponse.Message);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateAsync(UpdateProductionDto updateProductionDto)
        {
            var updateResponse = await _productionService.UpdateAsync(updateProductionDto);
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
