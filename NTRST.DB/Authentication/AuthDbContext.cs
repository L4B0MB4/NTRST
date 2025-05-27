using Microsoft.EntityFrameworkCore;
using NTRST.Models.Authentication;
using NTRST.Models.Authentication.Internal;

namespace NTRST.DB.Authentication;

public class AuthDbContext : DbContext
{
    public DbSet<AuthenticationToken> Tokens { get; set; }
    public DbSet<User> Users { get; set; }

    public string DbPath { get; }

    public AuthDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var dbFolder = Path.Join(path, "NTRST");
        Directory.CreateDirectory(dbFolder);
        DbPath = Path.Join(dbFolder, "authentication.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Automatically apply all IEntityTypeConfiguration<> from another assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthenticationToken).Assembly);
    }
}

