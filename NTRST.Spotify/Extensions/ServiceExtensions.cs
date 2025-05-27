using Microsoft.Extensions.DependencyInjection;

namespace NTRST.Spotify.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddSpotifyServices(this IServiceCollection services)
    {
        return services.AddScoped<UserService>();
    }
}