namespace FamilyKitchen.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RecipeModelChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "Recipes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Creator",
                table: "Recipes");
        }
    }
}
