using System.Text.Json.Serialization;

namespace NTRST.Spotify.Models;

public class Cursors
{
    [JsonPropertyName("after")]
    public string After { get; set; }
    
    [JsonPropertyName("before")]
    public string Before { get; set; }
}