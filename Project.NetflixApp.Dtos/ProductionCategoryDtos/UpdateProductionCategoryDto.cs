using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Dtos.ProductionCategoryDtos
{
    public class UpdateProductionCategoryDto
    {
        public int Id { get; set; }

        public int ProductionId { get; set; }
        public int CategoryId { get; set; }
    }
}
