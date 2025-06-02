using System.Text.Json.Serialization;

namespace NTRST.Models.Authentication.Internal;

public class AuthenticationToken
{
    [JsonIgnore]
    public long Id { get; set; }
    [JsonPropertyName("access_token")]
    public required string? AccessToken { get; set; }
    [JsonPropertyName("token_type")]
    public string? TokenType { get; set; }
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }
    [JsonIgnore]
    public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
    public bool IsExpired => DateTime.UtcNow > IssuedAt.AddSeconds(ExpiresIn-100);
    
    //will not be part of refresh response
    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }
    [JsonPropertyName("scope")]
    public string? Scope { get; set; }
}