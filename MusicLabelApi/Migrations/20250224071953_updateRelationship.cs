using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicLabelApi.Migrations
{
    /// <inheritdoc />
    public partial class updateRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumArtist_Albums_AlbumsId",
                table: "AlbumArtist");

            migrationBuilder.DropForeignKey(
                name: "FK_AlbumArtist_Artists_ArtistsId",
                table: "AlbumArtist");

            migrationBuilder.RenameColumn(
                name: "ArtistsId",
                table: "AlbumArtist",
                newName: "ArtistId");

            migrationBuilder.RenameColumn(
                name: "AlbumsId",
                table: "AlbumArtist",
                newName: "AlbumId");

            migrationBuilder.RenameIndex(
                name: "IX_AlbumArtist_ArtistsId",
                table: "AlbumArtist",
                newName: "IX_AlbumArtist_ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumArtist_Albums_AlbumId",
                table: "AlbumArtist",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumArtist_Artists_ArtistId",
                table: "AlbumArtist",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumArtist_Albums_AlbumId",
                table: "AlbumArtist");

            migrationBuilder.DropForeignKey(
                name: "FK_AlbumArtist_Artists_ArtistId",
                table: "AlbumArtist");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "AlbumArtist",
                newName: "ArtistsId");

            migrationBuilder.RenameColumn(
                name: "AlbumId",
                table: "AlbumArtist",
                newName: "AlbumsId");

            migrationBuilder.RenameIndex(
                name: "IX_AlbumArtist_ArtistId",
                table: "AlbumArtist",
                newName: "IX_AlbumArtist_ArtistsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumArtist_Albums_AlbumsId",
                table: "AlbumArtist",
                column: "AlbumsId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumArtist_Artists_ArtistsId",
                table: "AlbumArtist",
                column: "ArtistsId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
