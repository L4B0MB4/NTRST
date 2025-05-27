namespace NTRST.Models.Tracks;

public class Track
{
    public string Name { get; set; }
    public string Artist { get; set; }
    public string ExternalId { get; set; }
    public string ExternalArtistId { get; set; }
    public string[] Genres { get; set; }
}