using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace to_do_list_project.Migrations
{
    public partial class addCat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Catigories",
                columns: new[] { "Id", "Name" },
                values: new object[] { "1", "Select Categories" });

            migrationBuilder.InsertData(
                table: "Catigories",
                columns: new[] { "Id", "Name" },
                values: new object[] { "others", "Others" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Catigories",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Catigories",
                keyColumn: "Id",
                keyValue: "others");
        }
    }
}
