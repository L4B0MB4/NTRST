using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTRST.DB.Tracks.Migrations
{
    /// <inheritdoc />
    public partial class AddScopeToTracks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Source",
                schema: "tracks",
                table: "Tracks",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Source",
                schema: "tracks",
                table: "Tracks");
        }
    }
}
