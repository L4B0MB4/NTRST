using System.Text.Json.Serialization;
using NTRST.Models.Authentication.Internal;

namespace NTRST.Models.Authentication;

public class User
{
    public Guid Id { get; set; }
    public string ExternalId { get; set; }
    [JsonIgnore]
    public AuthenticationToken SpotifyToken { get; set; } //could be list at some point
}