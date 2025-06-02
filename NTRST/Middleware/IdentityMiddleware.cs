using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using NTRST.DB.Auth.Authentication;
using NTRST.Spotify.Services;

namespace NTRST.Middleware;

public class IdentityMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, UserService userService,IdentityService identityService)
    {
        if (context.User.Identity.IsAuthenticated)
        {
            var userGuid =context.User.Claims.Single(x => x.Type ==ClaimTypes.NameIdentifier ).Value;
            var user = await userService.GetUser(Guid.Parse(userGuid));
            identityService.SetIdentity(user.SpotifyToken,user.Id);
        }
        await next(context);
    }
}