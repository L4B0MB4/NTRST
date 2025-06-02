using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NTRST.Models.Authentication.Internal;
using NTRST.Spotify.Http;
using NTRST.Spotify.Services;

namespace NTRST.Tracks;
[ApiController]
[Authorize]
[Route("/tracks/spotify")]
public class SpotifyTrackController(
    ILogger<SpotifyTrackController> logger, TrackService trackService) : ControllerBase
{
    
    [HttpGet("recently")]
    public async Task<ActionResult> Callback([FromQuery] OAuthCodeResponse? responseCodeAuth)
    {
        var results = await trackService.GetRecentlyPlayed();
        return Ok(results);
    }
}