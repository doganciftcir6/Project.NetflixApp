using Project.NetflixApp.Common.Utilities.Results.Abstract;
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
        Task<IResponse> InsertAsync(CreateProductionCategoryDto createProductionCategoryDto);
        Task<IResponse> UpdateAsync(UpdateProductionCategoryDto updateProductionCategoryDto);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<IEnumerable<GetProductionCategoryDto>>> GetAllAsync();
        Task<IDataResponse<GetProductionCategoryDto>> GetByIdAsync(int id);
    }
}
