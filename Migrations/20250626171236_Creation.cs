using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaterJug.Migrations
{
    /// <inheritdoc />
    public partial class Creation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Jug1Capacity = table.Column<int>(type: "INTEGER", nullable: false),
                    Jug2Capacity = table.Column<int>(type: "INTEGER", nullable: false),
                    Target = table.Column<int>(type: "INTEGER", nullable: false),
                    Solved = table.Column<bool>(type: "INTEGER", nullable: false),
                    SolvedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StepsJson = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "History");
        }
    }
}
