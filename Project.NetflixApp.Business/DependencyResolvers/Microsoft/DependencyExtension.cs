using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Business.Concrete;
using Project.NetflixApp.Business.Mapping.AutoMapper;
using Project.NetflixApp.Business.Validations.FluentValidation.CategoryValidations;
using Project.NetflixApp.Business.Validations.FluentValidation.CountryValidations;
using Project.NetflixApp.Business.Validations.FluentValidation.DuractionValidations;
using Project.NetflixApp.Business.Validations.FluentValidation.GenderValidations;
using Project.NetflixApp.Business.Validations.FluentValidation.OperationClaimValidations;
using Project.NetflixApp.Business.Validations.FluentValidation.ProductionCategoryValidations;
using Project.NetflixApp.Business.Validations.FluentValidation.ProductionCategoryValidator;
using Project.NetflixApp.Business.Validations.FluentValidation.ProductionCommnetConfigurations;
using Project.NetflixApp.Business.Validations.FluentValidation.ProductionCommnetValidations;
using Project.NetflixApp.Business.Validations.FluentValidation.ProductionValidations;
using Project.NetflixApp.Business.Validations.FluentValidation.RatingValidations;
using Project.NetflixApp.Business.Validations.FluentValidation.TypeEntityValidations;
using Project.NetflixApp.Business.Validations.FluentValidation.UserOperationClaimValidations;
using Project.NetflixApp.Business.Validations.FluentValidation.UserValidations;
using Project.NetflixApp.DataAccess.Contexts.EntityFramework;
using Project.NetflixApp.DataAccess.Repositories.Abstract;
using Project.NetflixApp.DataAccess.Repositories.Concrete;
using Project.NetflixApp.Dtos.CategoryDtos;
using Project.NetflixApp.Dtos.CountryDtos;
using Project.NetflixApp.Dtos.DuractionDtos;
using Project.NetflixApp.Dtos.GenderDtos;
using Project.NetflixApp.Dtos.OperationClaimDtos;
using Project.NetflixApp.Dtos.ProductionCategoryDtos;
using Project.NetflixApp.Dtos.ProductionCommentDtos;
using Project.NetflixApp.Dtos.ProductionDtos;
using Project.NetflixApp.Dtos.RatingDtos;
using Project.NetflixApp.Dtos.TypeEntityDtos;
using Project.NetflixApp.Dtos.UserDtos;
using Project.NetflixApp.Dtos.UserOperationClaimDtos;
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
            services.AddScoped<IAuthService, AuthManager>();

            //fluent validation
            services.AddTransient<IValidator<CreateCategoryDto>, CreateCategoryValidator>();
            services.AddTransient<IValidator<UpdateCategoryDto>, UpdateCategoryValidator>();
            services.AddTransient<IValidator<CreateCountryDto>, CreateCountryValidator>();
            services.AddTransient<IValidator<UpdateCountryDto>, UpdateCountryValidator>();
            services.AddTransient<IValidator<CreateDuractionDto>, CreateDuractionValidator>();
            services.AddTransient<IValidator<UpdateDuractionDto>, UpdateDuractionValidator>();
            services.AddTransient<IValidator<CreateGenderDto>, CreateGenderValidator>();
            services.AddTransient<IValidator<UpdateGenderDto>, UpdateGenderValidator>();
            services.AddTransient<IValidator<CreateOperationClaimDto>, CreateOperationClaimValidator>();
            services.AddTransient<IValidator<UpdateOperationClaimDto>, UpdateOperationClaimValidator>();
            services.AddTransient<IValidator<CreateProductionCategoryDto>, CreateProductionCategoryValidator>();
            services.AddTransient<IValidator<UpdateProductionCategoryDto>, UpdateProductionCategoryValidator>();
            services.AddTransient<IValidator<CreateProductionCommentDto>, CreateProductionCommentValidator>();
            services.AddTransient<IValidator<UpdateProductionCommentDto>, UpdateProductionCommentValidator>();
            services.AddTransient<IValidator<CreateProductionDto>, CreateProductionValidator>();
            services.AddTransient<IValidator<UpdateProductionDto>, UpdateProductionValidator>();
            services.AddTransient<IValidator<CreateRatingDto>, CreateRatingValidator>();
            services.AddTransient<IValidator<UpdateRatingDto>, UpdateRatingValidator>();
            services.AddTransient<IValidator<CreateTypeEntityDto>, CreateTypeEntityValidator>();
            services.AddTransient<IValidator<UpdateTypeEntityDto>, UpdateTypeEntityValidator>();
            services.AddTransient<IValidator<CreateUserOperationClaimDto>, CreateUserOperationClaimValidator>();
            services.AddTransient<IValidator<UpdateUserOperationClaimDto>, UpdateUserOperationClaimValidator>();
            services.AddTransient<IValidator<CreateUserDto>, CreateUserValidator>();
            services.AddTransient<IValidator<UpdateUserDto>, UpdateUserValidator>();
            services.AddTransient<IValidator<LoginUserDto>, LoginUserValidator>();
            services.AddTransient<IValidator<RegisterUserDto>, RegisterUserValidator>();

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
