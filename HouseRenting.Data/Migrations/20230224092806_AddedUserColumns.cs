using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRenting.Data.Migrations
{
    public partial class AddedUserColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FistName",
                table: "AspNetUsers",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "FistName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "48320b68-3b96-4a8a-8a4a-994fbfc9800d", "Todor", "Teodosiev", "AQAAAAEAACcQAAAAEDBQxrtDfXsNTcJMCQw7Jfz4uxW0WvStLaJsFYWQRUnDtQBui4edhkz4nNFqO9JhBA==", "96f044f3-32c2-46fe-b72e-c7a717cd8841" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "FistName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3376e86f-255b-4db3-ab4a-e60aa687bf27", "Raicho", "Kamarinchev", "AQAAAAEAACcQAAAAEOWn1FYoaUfIXTGpfedcgHmWXGb1fL7g9WCwhDhzefGu+XvETo+VTKXnHHUlT5AAig==", "4e2c7615-65c1-4f57-bc20-7a40ac6d4507" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FistName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7bc08984-8650-4091-9a0d-96441823c2ac", "AQAAAAEAACcQAAAAEFU8NbF+FIW+S+UXwZarY+W+2/3aq374Mx8nTfD74gg7Yqvi+WEa0Fp9yzfqWb6QhQ==", "27d47519-8afe-4b02-949f-cb393ffeff90" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ab405250-1ced-44c7-80d4-6e7c2fc3a392", "AQAAAAEAACcQAAAAEJQrr4Ql3y2FUjkRoXD7wJMzVgL8Xsl2z2LWAGTjFIKhszl3X3jAEY9BAA5km7MuTQ==", "d3ddda37-7705-415b-932c-c06a8a721401" });
        }
    }
}
