using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Entities
{
    public class Production
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Cast { get; set; }
        public string ReleaseYear { get; set; }
        public DateTime CreateDate { get; set; }

        //category
        public List<Category> Categories { get; set; }
        //type
        public int TypeId { get; set; }
        public Type Type { get; set; }
        //Country
        public int CountryId { get; set; }
        public Country Country { get; set; }
        //Rating
        public int RatingId { get; set; }
        public Rating Rating { get; set; }
        //Duraction
        public int DuractionId { get; set; }
        public Duraction Duraction { get; set; }
        //ProductionComment
        public List<ProductionComment> ProductionComments { get; set; }
        //ProductionCategory
        public List<ProductionCategory> ProductionCategories { get; set; }

    }
}
