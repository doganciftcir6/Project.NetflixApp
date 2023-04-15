using Project.NetflixApp.Dtos.CategoryDtos;
using Project.NetflixApp.Dtos.CountryDtos;
using Project.NetflixApp.Dtos.DuractionDtos;
using Project.NetflixApp.Dtos.ProductionCategoryDtos;
using Project.NetflixApp.Dtos.ProductionCommentDtos;
using Project.NetflixApp.Dtos.RatingDtos;
using Project.NetflixApp.Dtos.TypeEntityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Dtos.ProductionDtos
{
    public class GetProductionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Cast { get; set; }
        public string ReleaseYear { get; set; }
        public DateTime CreateDate { get; set; }

        public int TypeEntityId { get; set; }
        public GetTypeEntityDto TypeEntity { get; set; }
        public int CountryId { get; set; }
        public GetCountryDto Country { get; set; }
        public int RatingId { get; set; }
        public GetRatingDto Rating { get; set; }
        public int DuractionId { get; set; }
        public GetDuractionDto Duraction { get; set; }
        //çoka çok ilişkiyi includelamak burda automapperin profilinde yaptığım kullanım önemlidir.
        public List<GetCategoryDto> Categories { get; set; }
        //ProductionCategory çoka bir olan ilişkiyi includelamak.
        public List<GetProductionCommentDto> ProductionComments { get; set; }
    }
}
