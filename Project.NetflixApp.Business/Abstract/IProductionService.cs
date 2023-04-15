using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Dtos.ProductionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Abstract
{
    public interface IProductionService
    {
        Task<IResponse> InsertAsync(CreateProductionDto createProductionDto);
        Task<IResponse> UpdateAsync(UpdateProductionDto updateProductionDto);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<IEnumerable<GetProductionDto>>> GetAllAsync();
        Task<IDataResponse<IEnumerable<GetProductionDto>>> GetAllWithReliationsAsync();
        Task<IDataResponse<GetProductionDto>> GetByIdAsync(int id);
        Task<IDataResponse<GetProductionDto>> GetByIdWithReliationsAsync(int id);
    }
}
