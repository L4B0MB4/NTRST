using Microsoft.AspNetCore.Mvc;
using NTRST.Models.Authentication.Internal;
using NTRST.Spotify;
using NTRST.Spotify.Http;

namespace NTRST.Tracks;
[ApiController]
[Route("/tracks/spotify")]
public class SpotifyTrackController(
    ILogger<SpotifyTrackController> logger,AuthenticationClient client)
{
    
    [HttpGet("recently")]
    public async Task<ActionResult> Callback([FromQuery] OAuthCodeResponse? responseCodeAuth)
    {
        if (responseCodeAuth?.Code == null) return new BadRequestResult();
        

        return new RedirectResult("/scalar");
    }
}