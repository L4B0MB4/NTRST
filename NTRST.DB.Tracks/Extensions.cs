using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NTRST.DB.Tracks;

namespace NTRST.DB.Auth.Authentication;

public static class Extensions
{
    public static IServiceCollection AddTracksDbContext(this IServiceCollection services)
    {
        return services.AddDbContext<TracksDbContext>();
    }

    public static void MigrateTracksDatabase(this IHost host)
    {
        //temporary solution - not appropriate for production
        // https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli#apply-migrations-at-runtime
        using var scope = host.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<TracksDbContext>();
        db.Database.Migrate();
    }
}