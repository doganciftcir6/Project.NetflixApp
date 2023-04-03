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
        Task<CreateProductionCommentDto> InsertAsync(CreateProductionCommentDto createProductionCommentDto);
        Task<UpdateProductionCommentDto> UpdateAsync(UpdateProductionCommentDto updateProductionCommentDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<GetProductionCommentDto>> GetAllAsync();
        Task<GetProductionCommentDto> GetByIdAsync(int id);
    }
}
