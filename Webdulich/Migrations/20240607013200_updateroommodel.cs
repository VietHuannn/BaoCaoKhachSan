using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webdulich.Migrations
{
    /// <inheritdoc />
    public partial class updateroommodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Rooms",
                newName: "ImageURL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageURL",
                table: "Rooms",
                newName: "Image");
        }
    }
}
