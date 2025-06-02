using Microsoft.Extensions.DependencyInjection;
using NTRST.Spotify.Services;

namespace NTRST.Spotify.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddSpotifyServices(this IServiceCollection services)
    {
        services.AddScoped<IdentityService>();
        services.AddScoped<TrackService>();
        services.AddSingleton<AuthService>();
        return services.AddScoped<UserService>();
        
    }
}