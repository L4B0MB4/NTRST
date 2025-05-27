using System.Net.Http.Json;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NTRST.Models;
using NTRST.Models.Authentication;
using NTRST.Models.Authentication.Internal;
using NTRST.Models.Config;
using NTRST.Spotify.Models;

namespace NTRST.Spotify.Http;

public class AuthenticationClient(
    ILogger<AuthenticationClient> logger,
    IOptions<SsoConfiguration> config,
    HttpClient client,
    TokenRetrivalService tokenRetrivalService)
{


    public async Task<IEnumerable<PlaybackHistoryItem>> GetRecentlyPlayed()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "v1/me/player/recently-played?limit=50");
        request.Headers.Add("Authorization","Bearer "+ tokenRetrivalService.GetToken());
        var response = await client.SendAsync(request);
        if(!response.IsSuccessStatusCode) throw new UnauthorizedAccessException("Unsuccessful response: " + response.StatusCode);
        var res = await response.Content.ReadFromJsonAsync<PlaybackHistoryResponse>();
        return res.Items;
    }
    
    public async Task<Me> GetMe()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "v1/me");
        request.Headers.Add("Authorization","Bearer "+ tokenRetrivalService.GetToken());
        var response = await client.SendAsync(request);
        if(!response.IsSuccessStatusCode) throw new UnauthorizedAccessException("Unsuccessful response: " + response.StatusCode);
        var res = await response.Content.ReadFromJsonAsync<Me>();
        return res;

    }
    public async Task<AuthenticationToken> GetToken(string code, string redirectUri)
    {
        try
        {
            var spotifyConfig = config.Value.Spotify;
            if (spotifyConfig == null) throw new ArgumentNullException(nameof(spotifyConfig));
            var formContent = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
            {
                new("grant_type", "authorization_code"),
                new("code", code),
                new("redirect_uri", redirectUri),
                new("client_id", "client_id"),
                new("client_secret", "client_secret")
            });
            var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");
            request.Content = formContent;

            request.Headers.Add("Authorization",
                "Basic " + Convert.ToBase64String(
                    Encoding.UTF8.GetBytes($"{spotifyConfig.ClientId}:{spotifyConfig.ClientSecret}")));
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AuthenticationToken>();
            }
            
            throw new UnauthorizedAccessException("Unsuccessful response: " + response.StatusCode + " for token retrival");
        }
        catch (Exception e)
        {
            logger.LogError("Exception during token retrival {e}",e);
            throw;
        }

    }
}