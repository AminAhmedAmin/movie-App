using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace moveis.Migrations
{
    public partial class moviesgerna : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ganaras",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ganaras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    Storeline = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: false),
                    Poster = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    GanaraId = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_movies_Ganaras_GanaraId",
                        column: x => x.GanaraId,
                        principalTable: "Ganaras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_movies_GanaraId",
                table: "movies",
                column: "GanaraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movies");

            migrationBuilder.DropTable(
                name: "Ganaras");
        }
    }
}
