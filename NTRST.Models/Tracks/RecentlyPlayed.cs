namespace NTRST.Models.Tracks;

public class RecentlyPlayed
{
    public Guid UserId { get; set; }
    public Track Track { get; set; }
    public DateTime PlayedAt { get; set; }
}