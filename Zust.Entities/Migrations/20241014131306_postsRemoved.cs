using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zust.Entities.Migrations
{
    /// <inheritdoc />
    public partial class postsRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "Posts",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                VideoLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PublisherId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Posts", x => x.Id);
                table.ForeignKey(
                    name: "FK_Posts_AspNetUsers_PublisherId",
                    column: x => x.PublisherId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });
           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
