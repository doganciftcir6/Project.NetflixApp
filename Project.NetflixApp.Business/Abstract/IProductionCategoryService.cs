using Project.NetflixApp.Dtos.ProductionCategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Abstract
{
    public interface IProductionCategoryService
    {
        Task<CreateProductionCategoryDto> InsertAsync(CreateProductionCategoryDto createProductionCategoryDto);
        Task<UpdateProductionCategoryDto> UpdateAsync(UpdateProductionCategoryDto updateProductionCategoryDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<GetProductionCategoryDto>> GetAllAsync();
        Task<GetProductionCategoryDto> GetByIdAsync(int id);
    }
}
