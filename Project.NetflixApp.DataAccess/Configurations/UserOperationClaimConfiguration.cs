using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.DataAccess.Configurations
{
    public class UserOperationClaimConfiguration : IEntityTypeConfiguration<UserOperationClaim>
    {
        public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
        {
            //tekrarlı kayıt engelle
            builder.HasIndex(x => new 
            {
                x.UserId,
                x.OperationClaimId
            }).IsUnique();

            builder.HasOne(x => x.User).WithMany(x => x.UserOperationClaims).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.OperationClaim).WithMany(x => x.UserOperationClaims).HasForeignKey(x => x.OperationClaimId);
        }
    }
}
