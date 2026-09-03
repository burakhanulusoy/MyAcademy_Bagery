using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bagery.WebUI.Migrations
{
    /// <inheritdoc />
    public partial class mig_update_ContactMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CodeExpireDate",
                table: "ContactMessages",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "ContactMessages",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VerificationCode",
                table: "ContactMessages",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeExpireDate",
                table: "ContactMessages");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "ContactMessages");

            migrationBuilder.DropColumn(
                name: "VerificationCode",
                table: "ContactMessages");
        }
    }
}
