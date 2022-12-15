using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace database.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultTF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TimeFrames",
                columns: new[] { "Id", "Code", "Name", "Seconds" },
                values: new object[,]
                {
                    { (short)1, "1D", "Day", 86400 },
                    { (short)2, "1H", "Hour", 3600 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TimeFrames",
                keyColumn: "Id",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "TimeFrames",
                keyColumn: "Id",
                keyValue: (short)2);
        }
    }
}
