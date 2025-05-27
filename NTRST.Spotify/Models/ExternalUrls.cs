using System.Text.Json.Serialization;

namespace NTRST.Spotify.Models;

public class ExternalUrls
{
    [JsonPropertyName("spotify")]
    public string Spotify { get; set; }
}