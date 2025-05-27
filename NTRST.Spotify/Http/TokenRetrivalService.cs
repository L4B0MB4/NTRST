using Microsoft.Extensions.Logging;
using NTRST.Models.Authentication.Internal;
using NTRST.Models.Exceptions;

namespace NTRST.Spotify.Http;

public class TokenRetrivalService(ILogger<TokenRetrivalService> logger)
{
    public AuthenticationToken? Token { get; set; }

    public string? GetToken()
    {
        if (Token == null)
        {
            throw new TokenNotSetException("Authentication Token was not set");
        }

        if (Token.IsExpired) throw new TokenExpiredException("Authentication Token is expired");
        return Token.AccessToken;
    }
}