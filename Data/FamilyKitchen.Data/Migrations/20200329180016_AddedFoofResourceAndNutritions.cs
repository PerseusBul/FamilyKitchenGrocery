using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FamilyKitchen.Data.Migrations
{
    public partial class AddedFoofResourceAndNutritions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Allergens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NutritionDeclarations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Energy = table.Column<decimal>(nullable: true),
                    Fats = table.Column<decimal>(nullable: true),
                    SaturatedFats = table.Column<decimal>(nullable: true),
                    Carbohydrate = table.Column<decimal>(nullable: true),
                    Protein = table.Column<decimal>(nullable: true),
                    Sodium = table.Column<decimal>(nullable: true),
                    GroupIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionDeclarations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodResources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Availability = table.Column<int>(nullable: false),
                    EANCode = table.Column<decimal>(nullable: true),
                    ExpireDate = table.Column<DateTime>(nullable: true),
                    MetricSystemUnit = table.Column<int>(nullable: false),
                    Producer = table.Column<string>(nullable: true),
                    NutritionDeclarationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodResources_NutritionDeclarations_NutritionDeclarationId",
                        column: x => x.NutritionDeclarationId,
                        principalTable: "NutritionDeclarations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodResources_IsDeleted",
                table: "FoodResources",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_FoodResources_NutritionDeclarationId",
                table: "FoodResources",
                column: "NutritionDeclarationId");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionDeclarations_IsDeleted",
                table: "NutritionDeclarations",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allergens");

            migrationBuilder.DropTable(
                name: "FoodResources");

            migrationBuilder.DropTable(
                name: "NutritionDeclarations");
        }
    }
}
