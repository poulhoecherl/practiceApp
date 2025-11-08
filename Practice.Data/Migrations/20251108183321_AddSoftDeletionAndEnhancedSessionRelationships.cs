using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practice.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeletionAndEnhancedSessionRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Songs",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Sessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Drills",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "SessionDrills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SessionId = table.Column<int>(type: "INTEGER", nullable: false),
                    DrillId = table.Column<int>(type: "INTEGER", nullable: false),
                    SessionId1 = table.Column<int>(type: "INTEGER", nullable: false),
                    DrillId1 = table.Column<int>(type: "INTEGER", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PerformanceRating = table.Column<int>(type: "INTEGER", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    RowCreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RowCreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    RowModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RowModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DrillId2 = table.Column<int>(type: "INTEGER", nullable: false),
                    SessionId2 = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionDrills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionDrills_Drills_DrillId",
                        column: x => x.DrillId,
                        principalTable: "Drills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionDrills_Drills_DrillId1",
                        column: x => x.DrillId1,
                        principalTable: "Drills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionDrills_Drills_DrillId2",
                        column: x => x.DrillId2,
                        principalTable: "Drills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionDrills_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionDrills_Sessions_SessionId1",
                        column: x => x.SessionId1,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionDrills_Sessions_SessionId2",
                        column: x => x.SessionId2,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionSongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SessionId = table.Column<int>(type: "INTEGER", nullable: false),
                    SongId = table.Column<int>(type: "INTEGER", nullable: false),
                    SessionId1 = table.Column<int>(type: "INTEGER", nullable: false),
                    SongId1 = table.Column<int>(type: "INTEGER", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    RowCreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RowCreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    RowModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RowModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    SessionId2 = table.Column<int>(type: "INTEGER", nullable: false),
                    SongId2 = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionSongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionSongs_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionSongs_Sessions_SessionId1",
                        column: x => x.SessionId1,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionSongs_Sessions_SessionId2",
                        column: x => x.SessionId2,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionSongs_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionSongs_Songs_SongId1",
                        column: x => x.SongId1,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionSongs_Songs_SongId2",
                        column: x => x.SongId2,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionDrills_DrillId",
                table: "SessionDrills",
                column: "DrillId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionDrills_DrillId1",
                table: "SessionDrills",
                column: "DrillId1");

            migrationBuilder.CreateIndex(
                name: "IX_SessionDrills_DrillId2",
                table: "SessionDrills",
                column: "DrillId2");

            migrationBuilder.CreateIndex(
                name: "IX_SessionDrills_SessionId_DrillId",
                table: "SessionDrills",
                columns: new[] { "SessionId", "DrillId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SessionDrills_SessionId1",
                table: "SessionDrills",
                column: "SessionId1");

            migrationBuilder.CreateIndex(
                name: "IX_SessionDrills_SessionId2",
                table: "SessionDrills",
                column: "SessionId2");

            migrationBuilder.CreateIndex(
                name: "IX_SessionSongs_SessionId_SongId",
                table: "SessionSongs",
                columns: new[] { "SessionId", "SongId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SessionSongs_SessionId1",
                table: "SessionSongs",
                column: "SessionId1");

            migrationBuilder.CreateIndex(
                name: "IX_SessionSongs_SessionId2",
                table: "SessionSongs",
                column: "SessionId2");

            migrationBuilder.CreateIndex(
                name: "IX_SessionSongs_SongId",
                table: "SessionSongs",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionSongs_SongId1",
                table: "SessionSongs",
                column: "SongId1");

            migrationBuilder.CreateIndex(
                name: "IX_SessionSongs_SongId2",
                table: "SessionSongs",
                column: "SongId2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SessionDrills");

            migrationBuilder.DropTable(
                name: "SessionSongs");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Drills");
        }
    }
}
