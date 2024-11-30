using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrLeaveManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedLeaveType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LeaveTypes",
                columns: new[] { "Id", "DateCreated", "DateModified", "DefaultDays", "Name" },
                values: new object[] { 1, new DateTime(2024, 11, 30, 17, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 30, 17, 0, 0, 0, DateTimeKind.Unspecified), 10, "Vacation" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
