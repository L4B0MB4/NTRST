using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NTRST.DB.Auth.Authentication;
using NTRST.Models.Authentication;
using NTRST.Models.Authentication.Internal;
using NTRST.Spotify.Http;

namespace NTRST.Spotify.Services;

public class UserService(ILogger<UserService> logger,  MeClient meClient,AuthDbContext authDbContext)
{
    
    public async Task<User> SaveUser(AuthenticationToken token)
    {
        
        var me = await meClient.GetMe();
        //needs change once there are more authentication methods
        var user = await authDbContext.Users.Include(x=>x.SpotifyToken).FirstOrDefaultAsync(x=>x.ExternalId == me.Id);
        if (user != null)
        {
            var currentToken = user.SpotifyToken;
            user.SpotifyToken = token;
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
                SpotifyToken = token
            };
            authDbContext.Add(user);
            await authDbContext.SaveChangesAsync();
        }

        return user;
    }

    public async Task<User> GetUser(Guid userGuid)
    {
        var user = await authDbContext.Users.Include(x=>x.SpotifyToken).FirstOrDefaultAsync(x=>x.Id == userGuid);
        return user;
    }
}