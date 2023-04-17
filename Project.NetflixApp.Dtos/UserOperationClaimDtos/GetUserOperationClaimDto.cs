using Project.NetflixApp.Dtos.OperationClaimDtos;
using Project.NetflixApp.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Dtos.UserOperationClaimDtos
{
    public class GetUserOperationClaimDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public GetUserWithoutPasswordDto User { get; set; }
        public int OperationClaimId { get; set; }
        public GetOperationClaimDto OperationClaim { get; set; }
    }
}
