using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShop.Data.Migrations
{
    public partial class Seeding_IdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 28, 14, 1, 54, 746, DateTimeKind.Utc).AddTicks(3910),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 12, 28, 13, 13, 53, 5, DateTimeKind.Utc).AddTicks(5650));

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"), "79f2df57-9c43-445f-8621-2ff42b7affa8", "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"), new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DOB", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"), 0, "a72162a1-736c-4954-bbce-e3b5cf0e8563", new DateTime(1998, 10, 21, 17, 0, 0, 0, DateTimeKind.Utc), "hungnv.dev@gmail.com", true, "Hung", "Nguyen", false, null, "hungnv.dev@gmail.com", "admin", "AQAAAAEAACcQAAAAEPJ9HwcVN4qW94Nma59vNcjQLCS+U7dChsW7ahhtB9jbRMenODvrSPR+BAQ3qXkCMQ==", null, false, "", false, "admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 12, 28, 14, 1, 54, 747, DateTimeKind.Utc).AddTicks(4770));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"), new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 28, 13, 13, 53, 5, DateTimeKind.Utc).AddTicks(5650),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 12, 28, 14, 1, 54, 746, DateTimeKind.Utc).AddTicks(3910));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 12, 28, 13, 13, 53, 6, DateTimeKind.Utc).AddTicks(5070));
        }
    }
}
