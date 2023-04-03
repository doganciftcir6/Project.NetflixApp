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
        Task<CreateCategoryDto> InsertAsync(CreateCategoryDto createCategoryDto);
        Task<UpdateCategoryDto> UpdateAsync(UpdateCategoryDto updateCategoryDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<GetCategoryDto>> GetAllAsync();
        Task<GetCategoryDto> GetByIdAsync(int id);
    }
}
