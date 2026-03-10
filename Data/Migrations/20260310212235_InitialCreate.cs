using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    EligibleForHat = table.Column<bool>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Load",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Load", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Load_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stop",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LoadId = table.Column<long>(type: "INTEGER", nullable: false),
                    SequenceNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    AppointmentLocalEndTime = table.Column<string>(type: "TEXT", nullable: false),
                    AppointmentLocalStartTime = table.Column<string>(type: "TEXT", nullable: false),
                    AppointmentTimeZoneId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stop", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stop_Load_LoadId",
                        column: x => x.LoadId,
                        principalTable: "Load",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "CreatedAt", "EligibleForHat", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, "-9998-01-01 00:00:00", null, "Acme Corp", "-9998-01-01 00:00:00" },
                    { 2L, "-9998-01-01 00:00:00", null, "Globex Inc", "-9998-01-01 00:00:00" },
                    { 3L, "-9998-01-01 00:00:00", null, "Initech", "-9998-01-01 00:00:00" },
                    { 4L, "-9998-01-01 00:00:00", null, "Umbrella Corp", "-9998-01-01 00:00:00" },
                    { 5L, "-9998-01-01 00:00:00", null, "Stark Industries", "-9998-01-01 00:00:00" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Load_CustomerId",
                table: "Load",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Stop_LoadId",
                table: "Stop",
                column: "LoadId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stop");

            migrationBuilder.DropTable(
                name: "Load");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
