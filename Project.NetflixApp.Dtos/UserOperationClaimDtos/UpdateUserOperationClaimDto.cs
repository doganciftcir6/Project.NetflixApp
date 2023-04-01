using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Dtos.UserOperationClaimDtos
{
    public class UpdateUserOperationClaimDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
    }
}
