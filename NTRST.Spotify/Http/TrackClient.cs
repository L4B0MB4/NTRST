using System.Net.Http.Json;
using NTRST.Spotify.Models;
using NTRST.Spotify.Services;
using NTRST.Models.Extensions;

namespace NTRST.Spotify.Http;

public class TrackClient(
    HttpClient client,
    IdentityService identityService)
{
    public async Task<IEnumerable<PlaybackHistoryItem>> GetRecentlyPlayed(DateTime after)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"v1/me/player/recently-played?limit=50");
        request.Headers.Add("Authorization", "Bearer " + await identityService.GetToken());
        var response = await client.SendAsync(request);
        if (!response.IsSuccessStatusCode)
            throw new UnauthorizedAccessException("Unsuccessful response: " + response.StatusCode);
        var res = await response.Content.ReadFromJsonAsync<PlaybackHistoryResponse>();
        return res.Items;
    }
}