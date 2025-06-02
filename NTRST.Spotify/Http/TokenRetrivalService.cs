using Microsoft.Extensions.Logging;
using NTRST.DB.Authentication;
using NTRST.Models.Authentication.Internal;
using NTRST.Models.Exceptions;

namespace NTRST.Spotify.Http;

public class TokenRetrivalService(ILogger<TokenRetrivalService> logger, AuthenticationClient authenticationClient, AuthDbContext authDbContext)
{
    public AuthenticationToken? Token { get; set; }

    public async Task<string?> GetToken()
    {
        if (Token == null)
        {
            throw new TokenNotSetException("Authentication Token was not set");
        }

        if (Token.IsExpired)
        {
            var newToken = await authenticationClient.RefreshToken(Token.RefreshToken);
            var foundToken = authDbContext.Tokens.Single(x => x.RefreshToken == Token.RefreshToken);
            foundToken.AccessToken = newToken.AccessToken;
            foundToken.IssuedAt = newToken.IssuedAt;
            foundToken.ExpiresIn = newToken.ExpiresIn;
            authDbContext.Tokens.Update(foundToken);
            await authDbContext.SaveChangesAsync();
            Token = foundToken;
        }
        return Token.AccessToken;
    }
}