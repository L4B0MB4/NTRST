using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NTRST.DB.Authentication;
using NTRST.Models.Authentication;
using NTRST.Models.Authentication.Internal;
using NTRST.Spotify.Http;

namespace NTRST.Spotify;

public class UserService(ILogger<UserService> logger, AuthenticationClient spotifyAuthClient, TokenRetrivalService tokenRetrivalService,AuthDbContext authDbContext)
{
    
    public async Task Authenticate(OAuthCodeResponse responseCodeAuth,string redirectUrl)
    {
        var resp =await spotifyAuthClient.GetToken(responseCodeAuth.Code,redirectUrl);
        tokenRetrivalService.Token = resp;
        var me = await spotifyAuthClient.GetMe();
        //needs change once there are more authentication methods
        var user = await authDbContext.Users.Include(x=>x.SpotifyToken).FirstOrDefaultAsync(x=>x.ExternalId == me.Id);
        if (user != null)
        {
            var currentToken = user.SpotifyToken;
            user.SpotifyToken = resp;
            authDbContext.Update(user);
            authDbContext.Remove(currentToken);
            await authDbContext.SaveChangesAsync();
        }
        else
        {
            user = new User()
            {
                Id = Guid.NewGuid(),
                ExternalId = me.Id,
                SpotifyToken = resp
            };
            authDbContext.Add(user);
            await authDbContext.SaveChangesAsync();
        }

        var res = await spotifyAuthClient.GetRecentlyPlayed();
    }
}