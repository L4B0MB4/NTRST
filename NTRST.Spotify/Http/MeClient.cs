using System.Net.Http.Json;
using NTRST.Spotify.Models;
using NTRST.Spotify.Services;

namespace NTRST.Spotify.Http;

public class MeClient(
    HttpClient client,
    IdentityService identityService)
{
    public async Task<Me> GetMe()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "v1/me");
        request.Headers.Add("Authorization", "Bearer " + await identityService.GetToken());
        var response = await client.SendAsync(request);
        if (!response.IsSuccessStatusCode)
            throw new UnauthorizedAccessException("Unsuccessful response: " + response.StatusCode);
        var res = await response.Content.ReadFromJsonAsync<Me>();
        return res;
    }
}