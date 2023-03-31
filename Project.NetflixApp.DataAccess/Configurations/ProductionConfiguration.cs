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
    public class ProductionConfiguration : IEntityTypeConfiguration<Production>
    {
        public void Configure(EntityTypeBuilder<Production> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Director).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Cast).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.ReleaseYear).HasMaxLength(300).IsRequired();
            builder.Property(x => x.CreateDate).HasDefaultValueSql("getdate()");

            builder.HasOne(x => x.Country).WithMany(x => x.Productions).HasForeignKey(x => x.CountryId);
            builder.HasOne(x => x.Duraction).WithMany(x => x.Productions).HasForeignKey(x => x.DuractionId);
            builder.HasOne(x => x.Rating).WithMany(x => x.Productions).HasForeignKey(x => x.RatingId);
            builder.HasOne(x => x.TypeEntity).WithMany(x => x.Productions).HasForeignKey(x => x.TypeEntityId);
        }
    }
}
