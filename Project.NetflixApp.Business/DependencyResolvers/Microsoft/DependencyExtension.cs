using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Business.Concrete;
using Project.NetflixApp.Business.Mapping.AutoMapper;
using Project.NetflixApp.Business.Validations.FluentValidation.CategoryValidations;
using Project.NetflixApp.DataAccess.Contexts.EntityFramework;
using Project.NetflixApp.DataAccess.Repositories.Abstract;
using Project.NetflixApp.DataAccess.Repositories.Concrete;
using Project.NetflixApp.Dtos.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.DependencyResolvers.Microsoft
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //sql
            services.AddDbContext<NetflixAppContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("Local"));
            });
            //repo scopes
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IDuractionRepository, DuractionRepository>();
            services.AddScoped<IGenderRepository, GenderRepository>();
            services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
            services.AddScoped<IProductionCategoryRepository, ProductionCategoryRepository>();
            services.AddScoped<IProductionCommentRepository, ProductionCommentRepository>();
            services.AddScoped<IProductionRepository, ProductionRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<ITypeEntityRepository, TypeEntityRepository>();
            services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            //service scopes
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICountryService, CountryManager>();
            services.AddScoped<IDuractionService, DuractionManager>();
            services.AddScoped<IGenderService,  GenderManager>();
            services.AddScoped<IOperationClaimService, OperationClaimManager>();
            services.AddScoped<IProductionCategoryService, ProductionCategoryManager>();
            services.AddScoped<IProductionCommentService,  ProductionCommentManager>();
            services.AddScoped<IProductionService, ProductionManager>();
            services.AddScoped<IRatingService, RatingManager>();
            services.AddScoped<ITypeEntityService, TypeEntityManager>();
            services.AddScoped<IUserOperationClaimService,  UserOperationClaimManager>();
            services.AddScoped<IUserService, UserManager>();

            //fluent validation
            services.AddTransient<IValidator<CreateCategoryDto>, CreateCategoryValidator>();
            services.AddTransient<IValidator<UpdateCategoryDto>, UpdateCategoryValidator>();

            //automapper
            var mapperConfiguration = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new CategoryProfile());
                opt.AddProfile(new CountryProfile());
                opt.AddProfile(new DuractionProfile());
                opt.AddProfile(new GenderProfile());
                opt.AddProfile(new OperationClaimProfile());
                opt.AddProfile(new ProductionCategoryProfile());
                opt.AddProfile(new ProductionCommentProfile());
                opt.AddProfile(new ProductionProfile());
                opt.AddProfile(new RatingProfile());
                opt.AddProfile(new TypeEntityProfile());
                opt.AddProfile(new UserOperationClaimProfile());
                opt.AddProfile(new UserProfile());
            });
            //automapperı projeye kaydet.
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }

    }
}
