using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bagery.WebUI.Migrations
{
    /// <inheritdoc />
    public partial class mig_update_Blog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LongTitle",
                table: "Blogs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LongTitle",
                table: "Blogs");
        }
    }
}
