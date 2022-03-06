using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reporting.API.Migrations
{
    public partial class ReportFieldsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReportContent",
                table: "Report",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReportTime",
                table: "Report",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportContent",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "ReportTime",
                table: "Report");
        }
    }
}
