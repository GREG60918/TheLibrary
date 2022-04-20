using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheLibrary.Migrations
{
    public partial class ApplyNameToMembershipType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MembershipTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Pay as You Go");

            migrationBuilder.UpdateData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Monthly");

            migrationBuilder.UpdateData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Quarterly");

            migrationBuilder.UpdateData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Yearly");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "MembershipTypes");
        }
    }
}
