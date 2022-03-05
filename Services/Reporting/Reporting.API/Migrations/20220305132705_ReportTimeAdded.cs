using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reporting.API.Migrations
{
    public partial class ReportTimeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReportContent",
                table: "ReportItem",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestTime",
                table: "Report",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestTime",
                table: "Report");

            migrationBuilder.AlterColumn<string>(
                name: "ReportContent",
                table: "ReportItem",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
