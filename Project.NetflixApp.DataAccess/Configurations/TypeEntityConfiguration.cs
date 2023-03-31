using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.DataAccess.Configurations
{
    public class TypeEntityConfiguration : IEntityTypeConfiguration<TypeEntity>
    {
        public void Configure(EntityTypeBuilder<TypeEntity> builder)
        {
            builder.HasData(new TypeEntity[]
            {
                new()
                {
                    Id = 1,
                    Description = "Movie"
                },
                new()
                {
                    Id = 2,
                    Description = "Tv Show"
                },
            });
            builder.Property(x => x.Description).HasMaxLength(400).IsRequired();
        }
    }
}
