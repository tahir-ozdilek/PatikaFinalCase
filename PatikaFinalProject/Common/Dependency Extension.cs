using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PatikaFinalProject.Bussiness.Services;
using PatikaFinalProject.Controllers;
using PatikaFinalProject.DataAccess;
using PatikaFinalProject.Services.Mapper;
using PatikaFinalProject.Services.Validators;


namespace PatikaFinalProject.Common
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            MapperConfiguration configuration = new MapperConfiguration(opt => {
                                                                                    opt.AddProfile(new ShoppingListProfile());
                                                                                    opt.AddProfile(new CategoryProfile());
                                                                                    opt.AddProfile(new ProductProfile());
                                                                                });

            IMapper mapper = configuration.CreateMapper();

            services.AddSingleton(mapper);
            services.AddDbContext<MyDbContext>(opt =>
            {
                //opt.UseSqlServer("Data Source=TR33NBK161\\MSSQLSERVER01; Initial Catalog=finalCase; Integrated Security=true; TrustServerCertificate=True;"); 
                opt.UseSqlServer("Data Source=Dell; Initial Catalog=finalCase; Integrated Security=true; TrustServerCertificate=True;"); 
                opt.LogTo(Console.WriteLine, LogLevel.Information);
            });
            services.AddSingleton(mapper);
           
            services.AddTransient<IValidator<ShoppingListCreateDTO>, ShoppingListCreateDTOValidator>();
            services.AddTransient<IValidator<ShoppingListDTO>, ShoppingListDTOValidator>();
            services.AddTransient<IValidator<CategoryCreateDTO>, CategoryCreateDTOValidator>();
            services.AddTransient<IValidator<CategoryDTO>, CategoryDTOValidator>();
            services.AddTransient<IValidator<ProductCreateDTO>, ProductCreateDTOValidator>();
            services.AddTransient<IValidator<ProductDTO>, ProductDTOValidator>();
            services.AddTransient<IValidator<LoginRequestModel>, LoginRequestModelValidator>();
            services.AddTransient<IValidator<RegistrationRequestModel>, RegistrationModelValidator>();
            services.AddTransient<JWTGenerator, JWTGenerator>();

            services.AddScoped<ShoppingListService, ShoppingListService>();
         
        }
    }
}
