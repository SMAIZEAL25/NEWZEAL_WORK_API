using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NEWZEAL_LAND_WORK_API.Migrations
{
    /// <inheritdoc />
    public partial class AddingImaageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileExtension",
                table: "images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileExtension",
                table: "images");
        }
    }
}
