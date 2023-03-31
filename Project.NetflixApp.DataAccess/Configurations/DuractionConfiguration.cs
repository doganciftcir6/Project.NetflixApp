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
    public class DuractionConfiguration : IEntityTypeConfiguration<Duraction>
    {
        public void Configure(EntityTypeBuilder<Duraction> builder)
        {
            builder.HasData(new Duraction[]
            {
                new()
                {
                    Id = 1,
                    Description = "1 Season"
                },
                new()
                {
                    Id = 2,
                    Description = "2 Season"
                },
                new()
                {
                    Id = 3,
                    Description = "3 Season"
                },
                new()
                {
                    Id = 4,
                    Description = "90 min"
                },
                new()
                {
                    Id = 5,
                    Description = "94 min"
                }
            });
            builder.Property(x => x.Description).HasMaxLength(200).IsRequired();
        }
    }
}
