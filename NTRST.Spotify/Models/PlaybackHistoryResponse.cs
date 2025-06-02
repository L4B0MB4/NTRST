using System.Text.Json.Serialization;

namespace NTRST.Spotify.Models;


public class PlaybackHistoryResponse
{
    [JsonPropertyName("href")]
    public string Href { get; set; }
    
    [JsonPropertyName("limit")]
    public int Limit { get; set; }
    
    [JsonPropertyName("next")]
    public string Next { get; set; }
    
    [JsonPropertyName("cursors")]
    public Cursors Cursors { get; set; }
    
    [JsonPropertyName("total")]
    public int Total { get; set; }
    
    [JsonPropertyName("items")]
    public List<PlaybackHistoryItem> Items { get; set; }

}