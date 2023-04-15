using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Dtos.RatingDtos;
using System.Threading.Tasks;

namespace Project.NetflixApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;
        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var ratingResponse = await _ratingService.GetAllAsync();
            if(ratingResponse.ResponseType == ResponseType.Success)
            {
                return Ok(ratingResponse.Data);
            }
            return BadRequest();
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var ratingResponse = await _ratingService.GetByIdAsync(id);
            if(ratingResponse.ResponseType == ResponseType.NotFound)
            {
                return NotFound(ratingResponse.Message);
            }
            return Ok(ratingResponse.Data);
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleteResponse = await _ratingService.DeleteAsync(id);
            if(deleteResponse.ResponseType == ResponseType.NotFound)
            {
                return NotFound(deleteResponse.Message);
            }
            return Ok(deleteResponse.Message);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertAsync(CreateRatingDto createRatingDto)
        {
            var insertResponse = await _ratingService.InsertAsync(createRatingDto);
            if(insertResponse.ResponseType == ResponseType.ValidationError)
            {
                return BadRequest(insertResponse.CustomValidationErrors);
            }
            return Ok(insertResponse.Message);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateAsync(UpdateRatingDto updateRatingDto)
        {
            var updateResponse = await _ratingService.UpdateAsync(updateRatingDto);
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
