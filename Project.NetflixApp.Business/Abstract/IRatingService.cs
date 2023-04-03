using Project.NetflixApp.Dtos.RatingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Abstract
{
    public interface IRatingService
    {
        Task<CreateRatingDto> InsertAsync(CreateRatingDto createRatingDto);
        Task<UpdateRatingDto> UpdateAsync(UpdateRatingDto updateRatingDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<GetRatingDto>> GetAllAsync();
        Task<GetRatingDto> GetByIdAsync(int id);
    }
}
