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
        Task<CreateProductionDto> InsertAsync(CreateProductionDto createProductionDto);
        Task<UpdateProductionDto> UpdateAsync(UpdateProductionDto updateProductionDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<GetProductionDto>> GetAllAsync();
        Task<GetProductionDto> GetByIdAsync(int id);
    }
}
