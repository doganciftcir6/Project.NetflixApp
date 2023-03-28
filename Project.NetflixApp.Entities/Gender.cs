using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Entities
{
    public class Gender
    {
        public int Id { get; set; }
        public string Definition { get; set; }

        //user
        public List<User> Users { get; set; }
    }
}
