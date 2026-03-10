using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedHatEligibleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Acme Corp (Id=1): 3 loads, 5 stops, ~18 hours → hat eligible
            migrationBuilder.Sql("INSERT INTO Load (CustomerId) VALUES (1);");
            migrationBuilder.Sql("INSERT INTO Stop (LoadId, SequenceNumber, AppointmentLocalStartTime, AppointmentLocalEndTime, AppointmentTimeZoneId) VALUES ((SELECT MAX(Id) FROM Load), 1, datetime('now', '-3 days', '+8 hours'), datetime('now', '-3 days', '+11 hours'), 'America/Chicago');");
            migrationBuilder.Sql("INSERT INTO Stop (LoadId, SequenceNumber, AppointmentLocalStartTime, AppointmentLocalEndTime, AppointmentTimeZoneId) VALUES ((SELECT MAX(Id) FROM Load), 2, datetime('now', '-3 days', '+13 hours'), datetime('now', '-3 days', '+16 hours'), 'America/Chicago');");

            migrationBuilder.Sql("INSERT INTO Load (CustomerId) VALUES (1);");
            migrationBuilder.Sql("INSERT INTO Stop (LoadId, SequenceNumber, AppointmentLocalStartTime, AppointmentLocalEndTime, AppointmentTimeZoneId) VALUES ((SELECT MAX(Id) FROM Load), 1, datetime('now', '-5 days', '+7 hours'), datetime('now', '-5 days', '+11 hours'), 'America/New_York');");
            migrationBuilder.Sql("INSERT INTO Stop (LoadId, SequenceNumber, AppointmentLocalStartTime, AppointmentLocalEndTime, AppointmentTimeZoneId) VALUES ((SELECT MAX(Id) FROM Load), 2, datetime('now', '-5 days', '+14 hours'), datetime('now', '-5 days', '+17 hours'), 'America/New_York');");

            migrationBuilder.Sql("INSERT INTO Load (CustomerId) VALUES (1);");
            migrationBuilder.Sql("INSERT INTO Stop (LoadId, SequenceNumber, AppointmentLocalStartTime, AppointmentLocalEndTime, AppointmentTimeZoneId) VALUES ((SELECT MAX(Id) FROM Load), 1, datetime('now', '-7 days', '+6 hours'), datetime('now', '-7 days', '+11 hours'), 'America/Denver');");

            // Initech (Id=3): 3 loads, 4 stops, ~14 hours → hat eligible
            migrationBuilder.Sql("INSERT INTO Load (CustomerId) VALUES (3);");
            migrationBuilder.Sql("INSERT INTO Stop (LoadId, SequenceNumber, AppointmentLocalStartTime, AppointmentLocalEndTime, AppointmentTimeZoneId) VALUES ((SELECT MAX(Id) FROM Load), 1, datetime('now', '-2 days', '+9 hours'), datetime('now', '-2 days', '+13 hours'), 'America/Los_Angeles');");

            migrationBuilder.Sql("INSERT INTO Load (CustomerId) VALUES (3);");
            migrationBuilder.Sql("INSERT INTO Stop (LoadId, SequenceNumber, AppointmentLocalStartTime, AppointmentLocalEndTime, AppointmentTimeZoneId) VALUES ((SELECT MAX(Id) FROM Load), 1, datetime('now', '-4 days', '+8 hours'), datetime('now', '-4 days', '+13 hours'), 'America/Chicago');");
            migrationBuilder.Sql("INSERT INTO Stop (LoadId, SequenceNumber, AppointmentLocalStartTime, AppointmentLocalEndTime, AppointmentTimeZoneId) VALUES ((SELECT MAX(Id) FROM Load), 2, datetime('now', '-4 days', '+15 hours'), datetime('now', '-4 days', '+17 hours'), 'America/Chicago');");

            migrationBuilder.Sql("INSERT INTO Load (CustomerId) VALUES (3);");
            migrationBuilder.Sql("INSERT INTO Stop (LoadId, SequenceNumber, AppointmentLocalStartTime, AppointmentLocalEndTime, AppointmentTimeZoneId) VALUES ((SELECT MAX(Id) FROM Load), 1, datetime('now', '-6 days', '+10 hours'), datetime('now', '-6 days', '+13 hours'), 'America/New_York');");

            // Globex Inc (Id=2): 1 load only → NOT hat eligible (needs >2)
            migrationBuilder.Sql("INSERT INTO Load (CustomerId) VALUES (2);");
            migrationBuilder.Sql("INSERT INTO Stop (LoadId, SequenceNumber, AppointmentLocalStartTime, AppointmentLocalEndTime, AppointmentTimeZoneId) VALUES ((SELECT MAX(Id) FROM Load), 1, datetime('now', '-1 days', '+9 hours'), datetime('now', '-1 days', '+17 hours'), 'America/Chicago');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Stop;");
            migrationBuilder.Sql("DELETE FROM Load;");
        }
    }
}
