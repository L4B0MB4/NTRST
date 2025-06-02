using Microsoft.EntityFrameworkCore;
using NTRST.DB.Tracks;
using NTRST.Models.Tracks;
using NTRST.Spotify.Http;

namespace NTRST.Spotify.Services;

public class TrackService(TracksDbContext tracksDbContext, IdentityService identity, TrackClient trackClient)
{
    public async Task<IEnumerable<RecentlyPlayed>> GetRecentlyPlayed()
    {
        //take time from db and put it to the recently played list

        var newestRecentlyPlayed = await tracksDbContext.RecentlyPlayed.OrderByDescending(x => x.PlayedAt)
            .FirstOrDefaultAsync(x => x.UserId == identity.UserId);
        var results = await trackClient.GetRecentlyPlayed(newestRecentlyPlayed.PlayedAt);

        results = results.Where(x =>
            DateTime.Parse(x.PlayedAt) > (newestRecentlyPlayed?.PlayedAt ?? DateTime.MinValue));
        var recentlyPlayed = results.Select(x => new RecentlyPlayed()
        {
            PlayedAt = DateTime.Parse(x.PlayedAt),
            UserId = identity.UserId,
            Track = new Track()
            {
                CalculatedId = x.Track.Artists.First().Name + "_" + x.Track.Name,
                Name = x.Track.Name,
                Artist = x.Track.Artists.First().Name,
                ExternalArtistId = x.Track.Artists.First().Id,
                ExternalId = x.Track.Id,
                Genres = new string[] { },
                Source = "spotify"
            }
        });
        var recentlyPlayedUnique = recentlyPlayed.OrderByDescending(x => x.PlayedAt)
            .GroupBy(x => new { x.Track.Artist, x.Track.Name })
            .Select(x => x.First()).ToList();


        var trackIds = recentlyPlayedUnique.Select(x => x.Track.CalculatedId).ToList();
        var tracksInDb = tracksDbContext.Tracks.Where(x => trackIds.Contains(x.CalculatedId)).ToList();

        if (tracksInDb.Count > 0)
        {
            foreach (var recentlyPlayedItem in recentlyPlayedUnique)
            {
                var existingItem = tracksInDb
                    .FirstOrDefault(t =>
                        t.Artist == recentlyPlayedItem.Track.Artist && t.Name == recentlyPlayedItem.Track.Name);

                if (existingItem != null)
                {
                    recentlyPlayedItem.Track = existingItem;
                }
            }
        }

        tracksDbContext.RecentlyPlayed.AddRange(recentlyPlayedUnique);

        await tracksDbContext.SaveChangesAsync();
        var recentlyPlayedFromDb = tracksDbContext.RecentlyPlayed.Include(x => x.Track)
            .OrderByDescending(x => x.PlayedAt).Take(10).ToList().OrderByDescending(x => x.PlayedAt);

        return recentlyPlayedFromDb;
    }
}