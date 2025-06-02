using Microsoft.Extensions.DependencyInjection;
using NTRST.Spotify.Http;

namespace NTRST.Spotify.Extensions;

public static class HttpClientExtensions
{
    public static IServiceCollection AddSpotifyHttpClient(this IServiceCollection services)
    {
        services.AddScoped<TokenRetrivalService>();
        services.AddHttpClient<AuthenticationClient>(client =>
        {
            client.BaseAddress = new Uri("https://api.spotify.com");
        }).AddStandardResilienceHandler();
        services.AddHttpClient<MeClient>(client =>
        {
            client.BaseAddress = new Uri("https://api.spotify.com");
        }).AddStandardResilienceHandler();
        /*services.AddHttpClient<TrackClient>(client =>
        {
            client.BaseAddress = new Uri("https://api.spotify.com");
        }).AddStandardResilienceHandler();*/
        return services;
    }
}