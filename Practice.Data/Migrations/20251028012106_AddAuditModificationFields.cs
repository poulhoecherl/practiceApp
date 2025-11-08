using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practice.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditModificationFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RowModifiedBy",
                table: "Songs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RowModifiedOn",
                table: "Songs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RowModifiedBy",
                table: "Sessions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RowModifiedOn",
                table: "Sessions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RowModifiedBy",
                table: "Drills",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RowModifiedOn",
                table: "Drills",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowModifiedBy",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "RowModifiedOn",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "RowModifiedBy",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "RowModifiedOn",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "RowModifiedBy",
                table: "Drills");

            migrationBuilder.DropColumn(
                name: "RowModifiedOn",
                table: "Drills");
        }
    }
}
