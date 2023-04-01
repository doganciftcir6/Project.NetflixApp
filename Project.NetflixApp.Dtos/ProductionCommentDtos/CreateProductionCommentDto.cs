using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Dtos.ProductionCommentDtos
{
    public class CreateProductionCommentDto
    {
        public string Content { get; set; }

        public int UserId { get; set; }
        public int ProductionId { get; set; }
    }
}
