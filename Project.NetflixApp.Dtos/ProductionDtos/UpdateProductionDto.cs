using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Dtos.ProductionDtos
{
    public class UpdateProductionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Cast { get; set; }
        public string ReleaseYear { get; set; }

        public int TypeEntityId { get; set; }
        public int CountryId { get; set; }
        public int RatingId { get; set; }
        public int DuractionId { get; set; }
    }
}
