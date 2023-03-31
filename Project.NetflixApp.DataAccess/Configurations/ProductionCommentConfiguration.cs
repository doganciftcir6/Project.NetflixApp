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
    public class ProductionCommentConfiguration : IEntityTypeConfiguration<ProductionComment>
    {
        public void Configure(EntityTypeBuilder<ProductionComment> builder)
        {
            builder.Property(x => x.Content).HasMaxLength(900).IsRequired().HasColumnName("Content");
            builder.Property(x => x.CreateDate).HasDefaultValueSql("getdate()");

            builder.HasOne(x => x.User).WithMany(x => x.ProductionComments).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Production).WithMany(x => x.ProductionComments).HasForeignKey(x => x.ProductionId);
        }
    }
}
