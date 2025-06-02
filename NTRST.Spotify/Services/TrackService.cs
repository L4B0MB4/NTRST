using Microsoft.EntityFrameworkCore;
using NTRST.DB.Tracks;
using NTRST.Models.Tracks;
using NTRST.Spotify.Http;

namespace NTRST.Spotify.Services;

public class TrackService(TracksDbContext tracksDbContext, IdentityService identity, TrackClient trackClient)
{
    public async Task<IEnumerable<Track>> GetRecentlyPlayed()
    {
        var result = await trackClient.GetRecentlyPlayed();

        var recentlyPlayed = result.Select(x => new RecentlyPlayed()
        {
            PlayedAt = DateTime.Parse(x.PlayedAt),
            UserId = identity.UserId,
            Track = new Track()
            {
                Name = x.Track.Name,
                Artist = x.Track.Artists.First().Name,
                ExternalArtistId = x.Track.Artists.First().Id,
                ExternalId = x.Track.Id,
                Source = "spotify"
            }
        }).ToList();
        tracksDbContext.RecentlyPlayed.AddRange(recentlyPlayed);
        await tracksDbContext.SaveChangesAsync();
        var recentlyPlayedFromDb = tracksDbContext.RecentlyPlayed.Include(x => x.Track)
            .OrderByDescending(x => x.PlayedAt).Take(10)
            .Select(x => x.Track).ToList();

        return recentlyPlayedFromDb;
    }
}