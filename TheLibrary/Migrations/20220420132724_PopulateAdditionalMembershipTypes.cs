using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheLibrary.Migrations
{
    public partial class PopulateAdditionalMembershipTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MembershipTypes",
                columns: new[] { "Id", "DiscountRate", "DurationInMonths", "SignupFee" },
                values: new object[,]
                {
                    { 1, (byte)0, (byte)0, (short)0 },
                    { 2, (byte)10, (byte)1, (short)30 },
                    { 3, (byte)15, (byte)3, (short)40 },
                    { 4, (byte)20, (byte)12, (short)300 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
