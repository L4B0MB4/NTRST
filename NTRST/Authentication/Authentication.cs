using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NTRST.Models.API;
using NTRST.Models.Config;
using NTRST.Spotify.Http;
using NTRST.Spotify.Services;

namespace NTRST.Authentication;

[ApiController]
[Route("/authentication/ntrst")]
public class AuthenticationController(
    ILogger<AuthenticationController> logger,
    UserService userService,
    AuthService authService
) : ControllerBase
{
    [HttpPost("refresh")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<JwtTokenResponse>> RefreshToken()
    {
        try
        {
            if (!Request.Cookies.TryGetValue("refreshToken", out var userGuid))
            {
                return Unauthorized("Missing refresh token.");
            }

            var user = await userService.GetUser(Guid.Parse(userGuid));
            var jwt = authService.GenerateJwt(user.Id);
            return Ok(new JwtTokenResponse()
            {
                AccessToken = jwt
            });
        }
        catch (Exception e)
        {
            logger.LogError("Exception during refreshing the token {e}", e);
            return Unauthorized("Something went wrong during refreshing the token");
        }

    }
}