using System.Text.Json.Serialization;

namespace NTRST.Models.SSO;

public record ResponseTokenAuth
{
    [JsonPropertyName("access_token")]
    public required string? AccessToken { get; set; }
    [JsonPropertyName("token_type")]
    public string? TokenType { get; set; }
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }
    [JsonPropertyName("refresh_token")]
    public required string? RefreshToken { get; set; }
    [JsonPropertyName("scope")]
    public string? Scope { get; set; }
}