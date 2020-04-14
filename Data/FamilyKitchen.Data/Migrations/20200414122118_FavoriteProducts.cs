using Microsoft.EntityFrameworkCore.Migrations;

namespace FamilyKitchen.Data.Migrations
{
    public partial class FavoriteProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FamilyKitchenUsersFavoriteProducts",
                columns: table => new
                {
                    FamilyKitchenUserId = table.Column<string>(nullable: false),
                    ShopProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyKitchenUsersFavoriteProducts", x => new { x.FamilyKitchenUserId, x.ShopProductId });
                    table.ForeignKey(
                        name: "FK_FamilyKitchenUsersFavoriteProducts_AspNetUsers_FamilyKitchenUserId",
                        column: x => x.FamilyKitchenUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FamilyKitchenUsersFavoriteProducts_ShopProducts_ShopProductId",
                        column: x => x.ShopProductId,
                        principalTable: "ShopProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FamilyKitchenUsersFavoriteProducts_ShopProductId",
                table: "FamilyKitchenUsersFavoriteProducts",
                column: "ShopProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FamilyKitchenUsersFavoriteProducts");
        }
    }
}
