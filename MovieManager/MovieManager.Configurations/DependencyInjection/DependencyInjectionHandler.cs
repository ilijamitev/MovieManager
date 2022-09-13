using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MovieManager.DataAccess.Data;
using MovieManager.DataAccess.Repositories;
using MovieManager.DataAccess.Repositories.Interfaces;
using MovieManager.Domain.Models;
using MovieManager.Helpers.Validations;
using MovieManager.Mappers.MovieMapper;
using MovieManager.Mappers.UserMapper;
using MovieManager.ServiceModels.MovieServiceModels;
using MovieManager.ServiceModels.UserServiceModels;
using MovieManager.Services.Interfaces;
using MovieManager.Services.MovieService;
using MovieManager.Services.UserService;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MovieManager.Configurations.DependencyInjection;

public static class DependencyInjectionHandler
{
    public static IServiceCollection InjectDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<MovieManagerDbContext>(opt => opt.UseSqlServer(connectionString));
        return services;
    }
    public static IServiceCollection InjectAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UserProfile));
        services.AddAutoMapper(typeof(MovieProfile));
        return services;
    }

    public static IServiceCollection InjectFluentValidator(this IServiceCollection services)
    {
        services.AddScoped<IValidator<RegisterUserDto>, RegisterUserValidator>();
        services.AddScoped<IValidator<CreateMovieDto>, CreateMovieValidator>();
        return services;
    }

    public static IServiceCollection InjectServices(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ILoginRegisterService, LoginRegisterService>();
        services.AddTransient<IMovieService, MovieService>();
        return services;
    }

    public static IServiceCollection InjectRepositories(this IServiceCollection services)
    {
        services.AddTransient<IRepository<User>, UserRepository>();
        services.AddTransient<IRepository<Movie>, MoviesRepository>();
        return services;
    }

    public static IServiceCollection AddSwaggerConfiq(this IServiceCollection services)
    {
        services.AddSwaggerGen(
            c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                          Enter 'Bearer' [space] and then your token in the text input below.
                          \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                     {
                        {
                          new OpenApiSecurityScheme
                          {
                            Reference = new OpenApiReference
                              {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                              },
                              Scheme = "oauth2",
                              Name = "Bearer",
                              In = ParameterLocation.Header,

                            },
                            new List<string>()
                          }
                       });
            });
        return services;
    }
}
