using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bagery.WebUI.Migrations
{
    /// <inheritdoc />
    public partial class mig_updated_ourHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainImageUrl",
                table: "OurHistories",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImageUrl",
                table: "OurHistories");
        }
    }
}
