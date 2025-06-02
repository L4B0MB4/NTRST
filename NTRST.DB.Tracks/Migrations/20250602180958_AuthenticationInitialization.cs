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
                    CalculatedId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Artist = table.Column<string>(type: "TEXT", nullable: false),
                    ExternalId = table.Column<string>(type: "TEXT", nullable: false),
                    ExternalArtistId = table.Column<string>(type: "TEXT", nullable: false),
                    Genres = table.Column<string>(type: "TEXT", nullable: false),
                    Source = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.CalculatedId);
                });

            migrationBuilder.CreateTable(
                name: "RecentlyPlayed",
                schema: "tracks",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PlayedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TrackCalculatedId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecentlyPlayed", x => new { x.PlayedAt, x.UserId });
                    table.ForeignKey(
                        name: "FK_RecentlyPlayed_Tracks_TrackCalculatedId",
                        column: x => x.TrackCalculatedId,
                        principalSchema: "tracks",
                        principalTable: "Tracks",
                        principalColumn: "CalculatedId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecentlyPlayed_TrackCalculatedId",
                schema: "tracks",
                table: "RecentlyPlayed",
                column: "TrackCalculatedId");
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
