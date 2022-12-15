using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace database.Migrations
{
    /// <inheritdoc />
    public partial class DraftDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Priority = table.Column<short>(type: "smallint", nullable: false),
                    BaseClientUri = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeFrames",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    Name = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Seconds = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeFrames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockSourceMaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SourceCode = table.Column<string>(type: "text", nullable: false),
                    StockId = table.Column<int>(type: "integer", nullable: false),
                    SourceId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockSourceMaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockSourceMaps_Sources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Sources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockSourceMaps_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OpenValue = table.Column<int>(type: "integer", nullable: false),
                    OpenDecimalLen = table.Column<int>(type: "integer", nullable: false),
                    CloseValue = table.Column<int>(type: "integer", nullable: false),
                    CloseDecimalLen = table.Column<int>(type: "integer", nullable: false),
                    HighValue = table.Column<int>(type: "integer", nullable: false),
                    HighDecimalLen = table.Column<int>(type: "integer", nullable: false),
                    LowValue = table.Column<int>(type: "integer", nullable: false),
                    LowDecimalLen = table.Column<int>(type: "integer", nullable: false),
                    VolumeValue = table.Column<int>(type: "integer", nullable: false),
                    VolumeDecimalLen = table.Column<int>(type: "integer", nullable: false),
                    TimeFrameId = table.Column<short>(type: "smallint", nullable: false),
                    SourceId = table.Column<short>(type: "smallint", nullable: false),
                    StockId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quotes_Sources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Sources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quotes_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quotes_TimeFrames_TimeFrameId",
                        column: x => x.TimeFrameId,
                        principalTable: "TimeFrames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_SourceId",
                table: "Quotes",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_StockId_SourceId",
                table: "Quotes",
                columns: new[] { "StockId", "SourceId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_TimeFrameId",
                table: "Quotes",
                column: "TimeFrameId");

            migrationBuilder.CreateIndex(
                name: "IX_Sources_Priority",
                table: "Sources",
                column: "Priority",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_Code",
                table: "Stocks",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockSourceMaps_SourceId",
                table: "StockSourceMaps",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_StockSourceMaps_StockId_SourceId",
                table: "StockSourceMaps",
                columns: new[] { "StockId", "SourceId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quotes");

            migrationBuilder.DropTable(
                name: "StockSourceMaps");

            migrationBuilder.DropTable(
                name: "TimeFrames");

            migrationBuilder.DropTable(
                name: "Sources");

            migrationBuilder.DropTable(
                name: "Stocks");
        }
    }
}
