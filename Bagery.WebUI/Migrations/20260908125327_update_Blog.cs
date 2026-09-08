using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bagery.WebUI.Migrations
{
    /// <inheritdoc />
    public partial class update_Blog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastDescription",
                table: "Blogs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastDescriptionTitle",
                table: "Blogs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastDescription",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "LastDescriptionTitle",
                table: "Blogs");
        }
    }
}
