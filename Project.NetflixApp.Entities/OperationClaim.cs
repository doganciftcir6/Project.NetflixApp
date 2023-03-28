using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Entities
{
    public class OperationClaim
    {
        public int Id { get; set; }
        public string Description { get; set; }

        //UserOperationClaim
        public List<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
