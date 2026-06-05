using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsuarioApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // This migration assumes the AspNetUsers table already exists (from Identity)
            // We're just adding the LandingPageId column to link to MongoDB

            migrationBuilder.AddColumn<string>(
                name: "LandingPageId",
                table: "AspNetUsers",
                type: "varchar(255)",
                nullable: true,
                collation: "utf8mb4_unicode_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LandingPageId",
                table: "AspNetUsers");
        }
    }
}
