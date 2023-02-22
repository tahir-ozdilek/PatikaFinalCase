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
                                                                                    opt.AddProfile(new CustomerProfile());
                                                                                    opt.AddProfile(new ActorProfile());
                                                                                    opt.AddProfile(new DirectorProfile());
                                                                                    opt.AddProfile(new MovieProfile());
                                                                                    opt.AddProfile(new OrderMovieProfile());
                                                                                });

            IMapper mapper = configuration.CreateMapper();

            services.AddSingleton(mapper);
            services.AddDbContext<MyDbContext>(opt =>
            {
                opt.UseSqlServer("Data Source=Dell; Initial Catalog=patikaFinalProject; Integrated Security=true; TrustServerCertificate=True;"); 
                opt.LogTo(Console.WriteLine, LogLevel.Information);
            });
            services.AddSingleton(mapper);

            services.AddTransient<IValidator<CustomerCreateDTO>, CustomerCreateDTOValidator>();
            services.AddTransient<IValidator<ActorDTO>, ActorDTOValidator>();
            services.AddTransient<IValidator<ActorCreateDTO>, ActorCreateDTOValidator>();
            services.AddTransient<IValidator<MovieDTO>, MovieDTOValidator>();
            services.AddTransient<IValidator<MovieCreateDTO>, MovieCreateDTOValidator>();
            services.AddTransient<IValidator<DirectorDTO>, DirectorDTOValidator>();
            services.AddTransient<IValidator<DirectorCreateDTO>, DirectorCreateDTOValidator>();

            services.AddScoped<DirectorService, DirectorService>();
            services.AddScoped<CustomerService, CustomerService>();
            services.AddScoped<ActorService, ActorService>();
            services.AddScoped<MovieService, MovieService>();
            services.AddScoped<OrderMovieService, OrderMovieService>();
         
        }
    }
}
