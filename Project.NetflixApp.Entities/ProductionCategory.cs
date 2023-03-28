using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Entities
{
    public class ProductionCategory
    {
        public int Id { get; set; }

        public int ProductionId { get; set; }
        public Production Production { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
