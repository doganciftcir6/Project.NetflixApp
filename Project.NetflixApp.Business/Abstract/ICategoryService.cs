using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Dtos.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Abstract
{
    public interface ICategoryService
    {
        Task<IResponse> InsertAsync(CreateCategoryDto createCategoryDto);
        Task<IResponse> UpdateAsync(UpdateCategoryDto updateCategoryDto);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<IEnumerable<GetCategoryDto>>> GetAllAsync();
        Task<IDataResponse<GetCategoryDto>> GetByIdAsync(int id);
    }
}
