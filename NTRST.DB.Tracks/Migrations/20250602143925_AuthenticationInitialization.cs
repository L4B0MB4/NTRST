using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTRST.DB.Tracks.Migrations
{
    /// <inheritdoc />
    public partial class AuthenticationInitialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tracks");

            migrationBuilder.CreateTable(
                name: "Tracks",
                schema: "tracks",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Artist = table.Column<string>(type: "TEXT", nullable: false),
                    ExternalId = table.Column<string>(type: "TEXT", nullable: false),
                    ExternalArtistId = table.Column<string>(type: "TEXT", nullable: false),
                    Genres = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => new { x.Name, x.Artist });
                });

            migrationBuilder.CreateTable(
                name: "RecentlyPlayed",
                schema: "tracks",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PlayedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TrackName = table.Column<string>(type: "TEXT", nullable: false),
                    TrackArtist = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecentlyPlayed", x => new { x.PlayedAt, x.UserId });
                    table.ForeignKey(
                        name: "FK_RecentlyPlayed_Tracks_TrackName_TrackArtist",
                        columns: x => new { x.TrackName, x.TrackArtist },
                        principalSchema: "tracks",
                        principalTable: "Tracks",
                        principalColumns: new[] { "Name", "Artist" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecentlyPlayed_TrackName_TrackArtist",
                schema: "tracks",
                table: "RecentlyPlayed",
                columns: new[] { "TrackName", "TrackArtist" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecentlyPlayed",
                schema: "tracks");

            migrationBuilder.DropTable(
                name: "Tracks",
                schema: "tracks");
        }
    }
}
