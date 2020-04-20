using Microsoft.EntityFrameworkCore.Migrations;

namespace FamilyKitchen.Data.Migrations
{
    public partial class Change_CC_Dependency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FamilyKitchenUserId",
                table: "ClientCards",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FamilyKitchenUserId",
                table: "ClientCards",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
