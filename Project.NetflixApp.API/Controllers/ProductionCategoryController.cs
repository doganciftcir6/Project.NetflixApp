using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Dtos.ProductionCategoryDtos;
using System.Threading.Tasks;

namespace Project.NetflixApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionCategoryController : ControllerBase
    {
        private readonly IProductionCategoryService _productionCategoryService;
        public ProductionCategoryController(IProductionCategoryService productionCategoryService)
        {
            _productionCategoryService = productionCategoryService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var productionCategoryResponse = await _productionCategoryService.GetAllWithRelationsAsync();
            if(productionCategoryResponse.ResponseType == ResponseType.Success)
            {
                return Ok(productionCategoryResponse.Data);
            }
            return BadRequest();
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var productionCategoryResponse = await _productionCategoryService.GetByIdWithReliationsAsync(id);
            if(productionCategoryResponse.ResponseType == ResponseType.NotFound)
            {
                return NotFound(productionCategoryResponse.Message);
            }
            return Ok(productionCategoryResponse.Data);
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleteResponse = await _productionCategoryService.DeleteAsync(id);
            if(deleteResponse.ResponseType == ResponseType.NotFound)
            {
                return NotFound(deleteResponse.Message);
            }
            return Ok(deleteResponse.Message);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertAsync(CreateProductionCategoryDto createProductionCategoryDto)
        {
            var insertResponse = await _productionCategoryService.InsertAsync(createProductionCategoryDto);
            if(insertResponse.ResponseType == ResponseType.ValidationError)
            {
                return BadRequest(insertResponse.CustomValidationErrors);
            }
            return Ok(insertResponse.Message);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateAsync(UpdateProductionCategoryDto updateProductionCategoryDto)
        {
            var updateRespone = await _productionCategoryService.UpdateAsync(updateProductionCategoryDto);
            if(updateRespone.ResponseType == ResponseType.NotFound)
            {
                return NotFound(updateRespone.Message);
            }
            else if(updateRespone.ResponseType == ResponseType.ValidationError)
            {
                return BadRequest(updateRespone.CustomValidationErrors);
            }
            return Ok(updateRespone.Message);
        }
    }
}
