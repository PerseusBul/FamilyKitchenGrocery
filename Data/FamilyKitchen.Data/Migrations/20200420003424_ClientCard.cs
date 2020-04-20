using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FamilyKitchen.Data.Migrations
{
    public partial class ClientCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientCardId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClientCards",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Discount = table.Column<decimal>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    FamilyKitchenUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientCards_AspNetUsers_FamilyKitchenUserId",
                        column: x => x.FamilyKitchenUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientCards_FamilyKitchenUserId",
                table: "ClientCards",
                column: "FamilyKitchenUserId",
                unique: true,
                filter: "[FamilyKitchenUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ClientCards_IsDeleted",
                table: "ClientCards",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientCards");

            migrationBuilder.DropColumn(
                name: "ClientCardId",
                table: "AspNetUsers");
        }
    }
}
