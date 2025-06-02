using Microsoft.Extensions.Logging;
using NTRST.DB.Auth.Authentication;
using NTRST.Models.Authentication.Internal;
using NTRST.Models.Exceptions;
using NTRST.Spotify.Http;

namespace NTRST.Spotify.Services;

public class IdentityService(ILogger<IdentityService> logger, AuthenticationClient authenticationClient, AuthDbContext authDbContext)
{
    
    
    public Guid UserId { get; private set; }
    public AuthenticationToken? Token { get; private set; }

    public void SetIdentity(AuthenticationToken token, Guid userId)
    {
        Token = token;
        UserId = userId;
    }
    
    
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