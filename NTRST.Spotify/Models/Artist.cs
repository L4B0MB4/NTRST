using System.Text.Json.Serialization;

namespace NTRST.Spotify.Models;

public class Artist
{
    [JsonPropertyName("external_urls")]
    public ExternalUrls ExternalUrls { get; set; }
    
    [JsonPropertyName("href")]
    public string Href { get; set; }
    
    [JsonPropertyName("id")]
    public string Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [JsonPropertyName("uri")]
    public string Uri { get; set; }
}