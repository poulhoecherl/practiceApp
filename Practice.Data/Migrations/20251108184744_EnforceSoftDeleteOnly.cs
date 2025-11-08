using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practice.Data.Migrations
{
    /// <inheritdoc />
    public partial class EnforceSoftDeleteOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Disable foreign key constraints temporarily
            migrationBuilder.Sql("PRAGMA foreign_keys = OFF;", suppressTransaction: true);

            migrationBuilder.DropForeignKey(
                name: "FK_SessionDrills_Drills_DrillId",
                table: "SessionDrills");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionDrills_Sessions_SessionId",
                table: "SessionDrills");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionSongs_Sessions_SessionId",
                table: "SessionSongs");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionSongs_Songs_SongId",
                table: "SessionSongs");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionDrills_Drills_DrillId",
                table: "SessionDrills",
                column: "DrillId",
                principalTable: "Drills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionDrills_Sessions_SessionId",
                table: "SessionDrills",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionSongs_Sessions_SessionId",
                table: "SessionSongs",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionSongs_Songs_SongId",
                table: "SessionSongs",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            // Disable foreign key constraints temporarily
            migrationBuilder.Sql("PRAGMA foreign_keys = ON;", suppressTransaction: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionDrills_Drills_DrillId",
                table: "SessionDrills");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionDrills_Sessions_SessionId",
                table: "SessionDrills");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionSongs_Sessions_SessionId",
                table: "SessionSongs");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionSongs_Songs_SongId",
                table: "SessionSongs");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionDrills_Drills_DrillId",
                table: "SessionDrills",
                column: "DrillId",
                principalTable: "Drills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionDrills_Sessions_SessionId",
                table: "SessionDrills",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionSongs_Sessions_SessionId",
                table: "SessionSongs",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionSongs_Songs_SongId",
                table: "SessionSongs",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
