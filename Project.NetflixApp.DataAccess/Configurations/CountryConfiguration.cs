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
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(new Country[]
            {
                new()
                {
                    Id = 1,
                    Description = "United States"
                },
                new()
                {
                    Id = 2,
                    Description = "India"
                },
                new()
                {
                    Id = 3,
                    Description = "United Kingdom"
                },
                new()
                {
                    Id = 4,
                    Description = "Japan"
                },
                new()
                {
                    Id = 5,
                    Description = "South Korea"
                }
            });
            builder.Property(x => x.Description).HasMaxLength(200).IsRequired();
        }
    }
}
