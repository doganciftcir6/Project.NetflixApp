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
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.HasData(new Gender[]
            {
                new()
                {
                    Id = 1,
                    Definition = "Male"
                },
                new()
                {
                    Id = 2,
                    Definition = "Female"
                },
                new()
                {
                    Id = 3,
                    Definition = "I do not want to specify"
                }
            });
            builder.Property(x => x.Definition).HasMaxLength(100).IsRequired();
        }
    }
}
