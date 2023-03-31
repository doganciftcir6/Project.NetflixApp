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
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasData(new Rating[]
            {
                new()
                {
                    Id = 1,
                    Description = "TV-MA"
                },
                new()
                {
                    Id = 2,
                    Description = "TV-14"
                },
                new()
                {
                    Id = 3,
                    Description = "TV-PG"
                },
                new()
                {
                    Id = 4,
                    Description = "R"
                },
                new()
                {
                    Id = 5,
                    Description = "PG-13"
                }
            });
            builder.Property(x => x.Description).HasMaxLength(400).IsRequired();
        }
    }
}
