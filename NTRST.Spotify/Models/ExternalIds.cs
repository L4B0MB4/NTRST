using System.Text.Json.Serialization;

namespace NTRST.Spotify.Models;

public class ExternalIds
{
    [JsonPropertyName("isrc")]
    public string Isrc { get; set; }
    
    [JsonPropertyName("ean")]
    public string Ean { get; set; }
    
    [JsonPropertyName("upc")]
    public string Upc { get; set; }
}