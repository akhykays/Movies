using Microsoft.Extensions.DependencyInjection;
using MovieManagement.Application.Movies;

namespace MovieManagement.Application.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IMovieService, MovieService>();
        return services;
    }
}
