using Microsoft.EntityFrameworkCore;
using Project.NetflixApp.DataAccess.Configurations;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.DataAccess.Contexts.EntityFramework
{
    public class NetflixAppContext : DbContext
    {
        //contexti dependecylemek için
        public NetflixAppContext(DbContextOptions<NetflixAppContext> options) : base(options) 
        { 

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Duraction> Duractions { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<Production> Productions { get; set; }
        public DbSet<ProductionCategory> ProductionCategories { get; set; }
        public DbSet<ProductionComment> ProductionComments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<TypeEntity> TypeEntities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new DuractionConfiguration());
            modelBuilder.ApplyConfiguration(new GenderConfiguration());
            modelBuilder.ApplyConfiguration(new OperationClaimConfiguration());
            modelBuilder.ApplyConfiguration(new ProductionConfiguration());
            modelBuilder.ApplyConfiguration(new ProductionCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductionCommentConfiguration());
            modelBuilder.ApplyConfiguration(new RatingConfiguration());
            modelBuilder.ApplyConfiguration(new TypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserOperationClaimConfiguration());
        }
    }
}
