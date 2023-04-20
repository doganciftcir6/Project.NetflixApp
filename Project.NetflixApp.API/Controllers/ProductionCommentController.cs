using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Dtos.ProductionCommentDtos;
using System.Threading.Tasks;

namespace Project.NetflixApp.API.Controllers
{
    [Authorize(Roles = "Admin, Member")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionCommentController : ControllerBase
    {
        private readonly IProductionCommentService _productionCommentService;
        public ProductionCommentController(IProductionCommentService productionCommentService)
        {
            _productionCommentService = productionCommentService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var productionCommentResponse = await _productionCommentService.GetAllWithRelationsAsync();
            if(productionCommentResponse.ResponseType == ResponseType.Success)
            {
                return Ok(productionCommentResponse.Data);
            }
            return BadRequest();
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var productionCommentResponse = await _productionCommentService.GetByIdWithRelationsAsync(id);
            if(productionCommentResponse.ResponseType == ResponseType.NotFound)
            {
                return NotFound(productionCommentResponse.Message);
            }
            return Ok(productionCommentResponse.Data);
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleteResponse = await _productionCommentService.DeleteAsync(id);
            if(deleteResponse.ResponseType == ResponseType.NotFound)
            {
                return NotFound(deleteResponse.Message);
            }
            return Ok(deleteResponse.Message);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertAsync(CreateProductionCommentDto createProductionCommentDto)
        {
            var insertResponse = await _productionCommentService.InsertAsync(createProductionCommentDto);
            if(insertResponse.ResponseType == ResponseType.ValidationError)
            {
                return BadRequest(insertResponse.CustomValidationErrors);
            }
            return Ok(insertResponse.Message);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateAsync(UpdateProductionCommentDto updateProductionCommentDto)
        {
            var updateResponse = await _productionCommentService.UpdateAsync(updateProductionCommentDto);
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
