using Microsoft.Extensions.Configuration;

namespace NTRST.Models.Config;

public class AuthConfiguration
{
    [ConfigurationKeyName("signing_secret")]
    public string SigningSecret { get; set; }
}