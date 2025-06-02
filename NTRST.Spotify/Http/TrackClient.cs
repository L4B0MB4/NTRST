using System.Net.Http.Json;
using NTRST.Spotify.Models;

namespace NTRST.Spotify.Http;

public class TrackClient(
    HttpClient client,
    TokenRetrivalService tokenRetrivalService)
{
    public async Task<IEnumerable<PlaybackHistoryItem>> GetRecentlyPlayed()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "v1/me/player/recently-played?limit=50");
        request.Headers.Add("Authorization", "Bearer " + await tokenRetrivalService.GetToken());
        var response = await client.SendAsync(request);
        if (!response.IsSuccessStatusCode)
            throw new UnauthorizedAccessException("Unsuccessful response: " + response.StatusCode);
        var res = await response.Content.ReadFromJsonAsync<PlaybackHistoryResponse>();
        return res.Items;
    }
}