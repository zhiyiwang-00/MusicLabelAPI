using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicLabelApi.Migrations
{
    /// <inheritdoc />
    public partial class updateDBwithArtist2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StageName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Biography = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artists");
        }
    }
}
