using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reporting.API.Migrations
{
    public partial class ReportItemFieldsChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportContent",
                table: "ReportItem");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "ReportItem",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfContacts",
                table: "ReportItem",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPhones",
                table: "ReportItem",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "ReportItem");

            migrationBuilder.DropColumn(
                name: "NumberOfContacts",
                table: "ReportItem");

            migrationBuilder.DropColumn(
                name: "NumberOfPhones",
                table: "ReportItem");

            migrationBuilder.AddColumn<string>(
                name: "ReportContent",
                table: "ReportItem",
                type: "text",
                nullable: true);
        }
    }
}
