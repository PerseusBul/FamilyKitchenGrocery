using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FamilyKitchen.Data.Migrations
{
    public partial class Products_FRvRecipes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodResourcesRecipes",
                columns: table => new
                {
                    FoodResourceId = table.Column<int>(nullable: false),
                    RecipeId = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodResourcesRecipes", x => new { x.FoodResourceId, x.RecipeId });
                    table.ForeignKey(
                        name: "FK_FoodResourcesRecipes_FoodResources_FoodResourceId",
                        column: x => x.FoodResourceId,
                        principalTable: "FoodResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FoodResourcesRecipes_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShopProducts",
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
                    TradeMark = table.Column<string>(nullable: true),
                    RecipeId = table.Column<int>(nullable: true),
                    NutritionDeclarationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopProducts_NutritionDeclarations_NutritionDeclarationId",
                        column: x => x.NutritionDeclarationId,
                        principalTable: "NutritionDeclarations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShopProducts_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodResourcesRecipes_RecipeId",
                table: "FoodResourcesRecipes",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopProducts_IsDeleted",
                table: "ShopProducts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ShopProducts_NutritionDeclarationId",
                table: "ShopProducts",
                column: "NutritionDeclarationId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopProducts_RecipeId",
                table: "ShopProducts",
                column: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodResourcesRecipes");

            migrationBuilder.DropTable(
                name: "ShopProducts");
        }
    }
}
