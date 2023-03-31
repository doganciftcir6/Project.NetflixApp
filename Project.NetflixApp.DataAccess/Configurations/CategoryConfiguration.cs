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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category[]
            {
                new()
                {
                    Id = 1,
                    Description = "Action"
                },
                new()
                {
                    Id = 2,
                    Description = "Documentary"
                },
                new()
                {
                    Id = 3,
                    Description = "Science Fiction"
                },
                new()
                {
                    Id = 4,
                    Description = "Religious"
                },
                new()
                {
                    Id = 5,
                    Description = "Dramatic"
                },
                new()
                {
                    Id = 6,
                    Description = "Education"
                },
                new()
                {
                    Id = 7,
                    Description = "Erotic"
                },
                new()
                {
                    Id = 8,
                    Description = "Fantastic"
                },
                new()
                {
                    Id = 9,
                    Description = "Thriller"
                },
                new()
                {
                    Id = 10,
                    Description = "Comedy"
                },
                new()
                {
                    Id = 11,
                    Description = "Horror"
                },
                new()
                {
                    Id = 12,
                    Description = "Adventure"
                },
                new()
                {
                    Id = 13,
                    Description = "Music"
                },
                new()
                {
                    Id = 14,
                    Description = "Political"
                },
                new()
                {
                    Id = 15,
                    Description = "Propaganda"
                },
                new()
                {
                    Id = 16,
                    Description = "Romantic"
                },
                new()
                {
                    Id = 17,
                    Description = "War"
                },
                new()
                {
                    Id = 18,
                    Description = "Sport"
                },
                new()
                {
                    Id = 19,
                    Description = "Crime"
                },
                new()
                {
                    Id = 20,
                    Description = "Historical"
                },
                new()
                {
                    Id = 22,
                    Description = "Life is Narrative"
                }
            });
            builder.Property(x => x.Description).HasMaxLength(200).IsRequired();
        }
    }
}
