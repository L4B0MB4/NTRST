using System.Text.Json.Serialization;

namespace NTRST.Spotify.Models;

public class Restrictions
{
    [JsonPropertyName("reason")]
    public string Reason { get; set; }
}