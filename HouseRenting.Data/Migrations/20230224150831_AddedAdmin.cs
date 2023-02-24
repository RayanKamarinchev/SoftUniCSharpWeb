using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRenting.Data.Migrations
{
    public partial class AddedAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7cce6db2-7e90-481d-8a24-ca018f2ac78d", "AQAAAAEAACcQAAAAEMoCbIRmG2z2YQndhw8Z6JT0FREe5D7Y7NCmSgfpthxHuXez9IZ41St8onwHZTfaLg==", "0625509f-2f2a-49ce-b36d-975bf980fd76" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "db2a4e25-cf96-457d-b4e8-6728d40cb4b7", "AQAAAAEAACcQAAAAEE3+ZqIdLe02hB/i9gWQ0Rwx9eXVFnZjGZcK/pP40o30urIE8PPbjVkydZ/xUU3xQw==", "d1a19cc0-d594-4bb4-ae93-8f82e9dcca58" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FistName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "bcb4f072-ecca-43c9-ab26-c060c6f364e4", 0, "de05ec92-7356-4c51-bd64-b85fbf3695cc", "admin@mail.com", false, "Great", "Admin", false, null, "admin@mail.com", "admin@mail.com", "AQAAAAEAACcQAAAAEL5m1dZ82Ca68K6fWEA50wblSqQXnpv1ywGsfS2cE4/MdZIN/iHQeGdKWxLRJ61IYA==", null, false, "58cd1f13-2654-4f76-a086-c52e8effea24", false, "admin@mail.com" });

            migrationBuilder.InsertData(
                table: "Agents",
                columns: new[] { "Id", "PhoneNumber", "UserId" },
                values: new object[] { 5, "+359123456789", "bcb4f072-ecca-43c9-ab26-c060c6f364e4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Agents",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcb4f072-ecca-43c9-ab26-c060c6f364e4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "48320b68-3b96-4a8a-8a4a-994fbfc9800d", "AQAAAAEAACcQAAAAEDBQxrtDfXsNTcJMCQw7Jfz4uxW0WvStLaJsFYWQRUnDtQBui4edhkz4nNFqO9JhBA==", "96f044f3-32c2-46fe-b72e-c7a717cd8841" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3376e86f-255b-4db3-ab4a-e60aa687bf27", "AQAAAAEAACcQAAAAEOWn1FYoaUfIXTGpfedcgHmWXGb1fL7g9WCwhDhzefGu+XvETo+VTKXnHHUlT5AAig==", "4e2c7615-65c1-4f57-bc20-7a40ac6d4507" });
        }
    }
}
