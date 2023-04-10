using Project.NetflixApp.Common.Utilities.Results.Abstract;
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
        Task<IResponse> InsertAsync(CreateRatingDto createRatingDto);
        Task<IResponse> UpdateAsync(UpdateRatingDto updateRatingDto);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<IEnumerable<GetRatingDto>>> GetAllAsync();
        Task<IDataResponse<GetRatingDto>> GetByIdAsync(int id);
    }
}
