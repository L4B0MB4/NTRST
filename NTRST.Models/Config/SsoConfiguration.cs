using Microsoft.Extensions.Configuration;

namespace NTRST.Models.Config;

public class SsoConfiguration
{
    [ConfigurationKeyName("baseUrl")] public string? BaseUrl { get; set; }

    [ConfigurationKeyName("spotify")] public SpotifyConfiguration? Spotify { get; set; }

    public class SpotifyConfiguration
    {
        [ConfigurationKeyName("client_id")] public string? ClientId { get; set; }

        [ConfigurationKeyName("client_secret")]
        public string? ClientSecret { get; set; }
    }
}