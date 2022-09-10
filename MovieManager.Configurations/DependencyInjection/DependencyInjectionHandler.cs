using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieManager.DataAccess.Data;
using MovieManager.Mappers.MovieMapper;
using MovieManager.Mappers.UserMapper;

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
}
