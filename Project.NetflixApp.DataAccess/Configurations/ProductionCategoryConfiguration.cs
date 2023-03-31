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
    public class ProductionCategoryConfiguration : IEntityTypeConfiguration<ProductionCategory>
    {
        public void Configure(EntityTypeBuilder<ProductionCategory> builder)
        {
            //tekrarlı kayıt engelle
            builder.HasIndex(x => new
            {
                x.ProductionId,
                x.CategoryId
            }).IsUnique();

            builder.HasOne(x => x.Production).WithMany(x => x.ProductionCategories).HasForeignKey(x => x.ProductionId);
            builder.HasOne(x => x.Category).WithMany(x => x.ProductionCategories).HasForeignKey(x => x.CategoryId);
        }
    }
}
