using System.Text.Json.Serialization;

namespace NTRST.Spotify.Models;

public class Track
{
    [JsonPropertyName("album")]
    public Album Album { get; set; }
    
    [JsonPropertyName("artists")]
    public List<Artist> Artists { get; set; }
    
    [JsonPropertyName("available_markets")]
    public List<string> AvailableMarkets { get; set; }
    
    [JsonPropertyName("disc_number")]
    public int DiscNumber { get; set; }
    
    [JsonPropertyName("duration_ms")]
    public int DurationMs { get; set; }
    
    [JsonPropertyName("explicit")]
    public bool Explicit { get; set; }
    
    [JsonPropertyName("external_ids")]
    public ExternalIds ExternalIds { get; set; }
    
    [JsonPropertyName("external_urls")]
    public ExternalUrls ExternalUrls { get; set; }
    
    [JsonPropertyName("href")]
    public string Href { get; set; }
    
    [JsonPropertyName("id")]
    public string Id { get; set; }
    
    [JsonPropertyName("is_playable")]
    public bool IsPlayable { get; set; }
    
    [JsonPropertyName("linked_from")]
    public object LinkedFrom { get; set; }
    
    [JsonPropertyName("restrictions")]
    public Restrictions Restrictions { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("popularity")]
    public int Popularity { get; set; }
    
    [JsonPropertyName("preview_url")]
    public string PreviewUrl { get; set; }
    
    [JsonPropertyName("track_number")]
    public int TrackNumber { get; set; }
    
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [JsonPropertyName("uri")]
    public string Uri { get; set; }
    
    [JsonPropertyName("is_local")]
    public bool IsLocal { get; set; }
}