using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Entities
{
    public class ProductionComment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        //user
        public int UserId { get; set; }
        public User User { get; set; }
        //production
        public int ProductionId { get; set; }
        public Production Production { get; set; }
    }
}
