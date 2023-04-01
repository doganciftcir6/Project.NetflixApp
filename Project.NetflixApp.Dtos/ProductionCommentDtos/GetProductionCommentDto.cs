using Project.NetflixApp.Dtos.ProductionDtos;
using Project.NetflixApp.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Dtos.ProductionCommentDtos
{
    public class GetProductionCommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
    
        public int UserId { get; set; }
        public GetUserDto User { get; set; }
        public int ProductionId { get; set; }
        public GetProductionDto Production { get; set; }
    }
}
