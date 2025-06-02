using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using NTRST.DB.Auth.Authentication;
using NTRST.Models.Authentication;
using NTRST.Models.Authentication.Internal;
using NTRST.Spotify.Http;

namespace NTRST.Spotify.Services;

public class UserService(ILogger<UserService> logger, IMemoryCache memoryCache, MeClient meClient,AuthDbContext authDbContext)
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
        var userCached = memoryCache.Get<User>(userGuid.ToString());
        if (userCached != null)return userCached;
        var user = await authDbContext.Users.Include(x=>x.SpotifyToken).FirstOrDefaultAsync(x=>x.Id == userGuid);
        memoryCache.Set(userGuid.ToString(), user, TimeSpan.FromMinutes(1));
        return user;
    }
}