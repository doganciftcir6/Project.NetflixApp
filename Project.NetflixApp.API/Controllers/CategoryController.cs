using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Dtos.CategoryDtos;
using System.Threading.Tasks;

namespace Project.NetflixApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var categoryResopnse = await _categoryService.GetAllAsync();
            if(categoryResopnse.ResponseType == ResponseType.Success)
            {
                return Ok(categoryResopnse.Data);
            }
            return BadRequest();
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var categoryResponse = await _categoryService.GetByIdAsync(id);
            if(categoryResponse.ResponseType == ResponseType.NotFound)
            {
                return NotFound(categoryResponse.Message);
            }
            return Ok(categoryResponse.Data);
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleteResponse = await _categoryService.DeleteAsync(id);
            if(deleteResponse.ResponseType == ResponseType.NotFound)
            {
                return NotFound(deleteResponse.Message);
            }
            return Ok(deleteResponse.Message);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertAsync(CreateCategoryDto createCategoryDto)
        {
            var insertResponse = await _categoryService.InsertAsync(createCategoryDto);
            if(insertResponse.ResponseType == ResponseType.ValidationError)
            {
                return BadRequest(insertResponse.CustomValidationErrors);
            }
            return Ok(insertResponse.Message);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateAsync(UpdateCategoryDto updateCategoryDto)
        {
            var updateResponse = await _categoryService.UpdateAsync(updateCategoryDto);
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
