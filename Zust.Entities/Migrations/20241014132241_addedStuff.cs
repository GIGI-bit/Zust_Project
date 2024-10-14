using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zust.Entities.Migrations
{
    /// <inheritdoc />
    public partial class addedStuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
               name: "CreateDate",
               table: "Posts",
               type: "datetime2",
               nullable: false,
               defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
            migrationBuilder.AddColumn<string>(
              name: "Status",
              table: "Posts",
              type: "nvarchar(max)",
              nullable: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
