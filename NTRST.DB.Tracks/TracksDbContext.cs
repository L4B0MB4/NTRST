using Microsoft.EntityFrameworkCore;
using NTRST.Models.Authentication;
using NTRST.Models.Authentication.Internal;
using NTRST.Models.Tracks;

namespace NTRST.DB.Tracks;

public class TracksDbContext : DbContext
{
    public DbSet<Track> Tracks { get; set; }
    public DbSet<RecentlyPlayed> RecentlyPlayed  { get; set; }

    public string DbPath { get; }

    public TracksDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var dbFolder = Path.Join(path, "NTRST");
        Directory.CreateDirectory(dbFolder);
        DbPath = Path.Join(dbFolder, "tracks.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tracks");
        // Automatically apply all IEntityTypeConfiguration<> from another assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthenticationToken).Assembly);

        modelBuilder.Entity<Track>().HasKey(x => new { x.Name, x.Artist });
        
        modelBuilder.Entity<RecentlyPlayed>().HasKey(x => new { x.PlayedAt, x.UserId });
    }
}

