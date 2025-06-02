using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NTRST.Models.Authentication;
using NTRST.Models.Authentication.Internal;
using NTRST.Models.Config;
using NTRST.Spotify;
using NTRST.Spotify.Http;

namespace NTRST.Authentication;

[ApiController]
[Route("/authentication/spotify")]
public class SpotifyAuthenticationController(
    ILogger<SpotifyAuthenticationController> logger,
    IOptions<SsoConfiguration> ssoConfigOption,
    UserService userService,
    AuthenticationClient spotifyAuthClient,
    TokenRetrivalService tokenRetrivalService
) : ControllerBase
{
    private string GetRedirectUrl()
    {
        var ssoConfig = ssoConfigOption.Value;

        return new Uri(new Uri(ssoConfig.BaseUrl), "/authentication/spotify/callback").ToString();
    }

    [HttpGet("authorize")]
    [ProducesResponseType(StatusCodes.Status307TemporaryRedirect)]
    public ActionResult Authorize()
    {
        var ssoConfig = ssoConfigOption.Value;

        var callbackUrl = GetRedirectUrl();
        var redirectQuery = QueryString.Create(new List<KeyValuePair<string, string?>>
        {
            new("response_type", "code"),
            new("client_id", ssoConfig.Spotify?.ClientId),
            new("redirect_uri", callbackUrl),
            new("state", Guid.NewGuid().ToString()),
            new("scope",
                "playlist-read-collaborative user-follow-read user-read-playback-position user-top-read user-read-recently-played user-library-read")
        });

        var builder = new UriBuilder();

        builder.Scheme = "https";
        builder.Host = "accounts.spotify.com";
        builder.Path = "/authorize";
        builder.Query = redirectQuery.ToString();
        var redirectUrl = builder.Uri.ToString();

        return new RedirectResult(redirectUrl);
    }

    [HttpGet("callback")]
    public async Task<ActionResult> Callback([FromQuery] OAuthCodeResponse? responseCodeAuth)
    {
        if (responseCodeAuth?.Code == null) return new BadRequestResult();
        var resp = await spotifyAuthClient.GetToken(responseCodeAuth.Code, GetRedirectUrl());
        tokenRetrivalService.Token = resp;
        await userService.SaveUser(resp);

        return new RedirectResult("/scalar");
    }
}