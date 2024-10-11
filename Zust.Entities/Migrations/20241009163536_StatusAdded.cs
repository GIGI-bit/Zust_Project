using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zust.Entities.Migrations
{
    /// <inheritdoc />
    public partial class StatusAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Posts");
        }
    }
}
