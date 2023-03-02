using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PatikaFinalCase.Bussiness.Services;
using PatikaFinalProject.Bussiness.Services;
using PatikaFinalProject.Controllers;
using PatikaFinalProject.DataAccess;
using PatikaFinalProject.Services.Mapper;
using PatikaFinalProject.Services.Validators;
using System.Text;
using System.Text.Json.Serialization;

namespace PatikaFinalProject.Common
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddControllers()
            .AddJsonOptions(o => o.JsonSerializerOptions
                .ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                                    {
                                        Title = "JWTToken_Auth_API",
                                        Version = "v1"
                                    });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                                        {
                                            Name = "Authorization",
                                            Type = SecuritySchemeType.ApiKey,
                                            Scheme = "Bearer",
                                            BearerFormat = "JWT",
                                            In = ParameterLocation.Header,
                                            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer exzcxv...\"",
                                        });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement 
                                        {
                                            {
                                                new OpenApiSecurityScheme {
                                                    Reference = new OpenApiReference {
                                                        Type = ReferenceType.SecurityScheme,
                                                            Id = "Bearer"
                                                    }
                                                },
                                                new string[] {}
                                            }
                                        });
            });
            services.AddOutputCache();

            MapperConfiguration configuration = new MapperConfiguration(opt =>
            {
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
            
            services.AddScoped<RegisterLoginService, RegisterLoginService>();
            services.AddScoped<ShoppingListService, ShoppingListService>();
            services.AddScoped<ShoppingListSearchService, ShoppingListSearchService>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: "_myAllowSpecificOrigins",
                   builder =>
                   {
                       builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost");
                   });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => {
                opt.RequireHttpsMetadata = false;
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidIssuer = "http://localhost",
                    ValidAudience = "http://localhost",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("How much is this static key secure ?")),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };
            });

            services.AddOutputCache(options =>
            {
                options.DefaultExpirationTimeSpan = TimeSpan.FromMinutes(10);
            });
        }
    }
}
