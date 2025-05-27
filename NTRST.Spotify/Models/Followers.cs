using System.Text.Json.Serialization;

namespace NTRST.Spotify.Models;

public class Followers
{
    [JsonPropertyName("href")]
    public string? Href { get; set; }
    
    [JsonPropertyName("total")]
    public int Total { get; set; }
}