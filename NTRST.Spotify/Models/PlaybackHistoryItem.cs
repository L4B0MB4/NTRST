using System.Text.Json.Serialization;

namespace NTRST.Spotify.Models;

public class PlaybackHistoryItem
{
    [JsonPropertyName("track")]
    public Track Track { get; set; }
    
    [JsonPropertyName("played_at")]
    public string PlayedAt { get; set; }
    
    [JsonPropertyName("context")]
    public Context Context { get; set; }
}