using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Dtos.ProductionCommentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Abstract
{
    public interface IProductionCommentService
    {
        Task<IResponse> InsertAsync(CreateProductionCommentDto createProductionCommentDto);
        Task<IResponse> UpdateAsync(UpdateProductionCommentDto updateProductionCommentDto);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<IEnumerable<GetProductionCommentDto>>> GetAllAsync();
        Task<IDataResponse<IEnumerable<GetProductionCommentDto>>> GetAllWithRelationsAsync();
        Task<IDataResponse<GetProductionCommentDto>> GetByIdAsync(int id);
        Task<IDataResponse<GetProductionCommentDto>> GetByIdWithRelationsAsync(int id);
    }
}
