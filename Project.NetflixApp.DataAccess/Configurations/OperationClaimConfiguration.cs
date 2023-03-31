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
    public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
    {
        public void Configure(EntityTypeBuilder<OperationClaim> builder)
        {
            builder.HasData(new OperationClaim[]
            {
                new()
                {
                    Id = 1,
                    Description = "Admin"
                },
                new()
                {
                    Id = 2,
                    Description = "Member"
                }
            });
            builder.Property(x => x.Description).HasMaxLength(100).IsRequired();
        }
    }
}
