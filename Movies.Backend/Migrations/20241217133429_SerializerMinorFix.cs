using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.Backend.Migrations
{
    /// <inheritdoc />
    public partial class SerializerMinorFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePathsSerialized",
                table: "Movies",
                newName: "ImagePaths");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePaths",
                table: "Movies",
                newName: "ImagePathsSerialized");
        }
    }
}
