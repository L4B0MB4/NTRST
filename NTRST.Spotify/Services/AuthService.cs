using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NTRST.Models.Config;

namespace NTRST.Spotify.Services;

public class AuthService(IOptions<AuthConfiguration> authConfig)
{
    public string GenerateJwt(Guid userId)
    {
        var secretKey = authConfig.Value.SigningSecret;
        if(secretKey== null || secretKey.Length<25)throw new ArgumentException("Signing secret is not set or too short (at least 25 characters)");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        // Signing credentials using HMACSHA256
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Sample claims
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString())
        };

        // Create the JWT
        var token = new JwtSecurityToken(
            issuer: "ntrst.api",        
            audience: "ntrst.ui",   
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        // Write the token as a string
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}