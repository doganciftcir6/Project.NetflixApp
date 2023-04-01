using Project.NetflixApp.Dtos.CategoryDtos;
using Project.NetflixApp.Dtos.ProductionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Dtos.ProductionCategoryDtos
{
    public class GetProductionCategoryDto
    {
        public int Id { get; set; }

        public int ProductionId { get; set; }
        public GetProductionDto Production { get; set; }
        public int CategoryId { get; set; }
        public GetCategoryDto Category { get; set; }
    }
}
