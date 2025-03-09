using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EFExample.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "races",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    distance_km = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_races", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "runners",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    country = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_runners", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "race_result",
                columns: table => new
                {
                    runner_id = table.Column<int>(type: "integer", nullable: false),
                    race_id = table.Column<int>(type: "integer", nullable: false),
                    bib_number = table.Column<int>(type: "integer", nullable: false),
                    position = table.Column<int>(type: "integer", nullable: false),
                    time = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_race_result", x => new { x.runner_id, x.race_id });
                    table.ForeignKey(
                        name: "fk_race_result_races_race_id",
                        column: x => x.race_id,
                        principalTable: "races",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_race_result_runners_runner_id",
                        column: x => x.runner_id,
                        principalTable: "runners",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "race_runner",
                columns: table => new
                {
                    races_id = table.Column<int>(type: "integer", nullable: false),
                    runners_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_race_runner", x => new { x.races_id, x.runners_id });
                    table.ForeignKey(
                        name: "fk_race_runner_races_races_id",
                        column: x => x.races_id,
                        principalTable: "races",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_race_runner_runners_runners_id",
                        column: x => x.runners_id,
                        principalTable: "runners",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_race_result_bib_number",
                table: "race_result",
                column: "bib_number");

            migrationBuilder.CreateIndex(
                name: "ix_race_result_race_id",
                table: "race_result",
                column: "race_id");

            migrationBuilder.CreateIndex(
                name: "ix_race_runner_runners_id",
                table: "race_runner",
                column: "runners_id");

            migrationBuilder.CreateIndex(
                name: "ix_races_date",
                table: "races",
                column: "date");

            migrationBuilder.CreateIndex(
                name: "ix_runners_email",
                table: "runners",
                column: "email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "race_result");

            migrationBuilder.DropTable(
                name: "race_runner");

            migrationBuilder.DropTable(
                name: "races");

            migrationBuilder.DropTable(
                name: "runners");
        }
    }
}
