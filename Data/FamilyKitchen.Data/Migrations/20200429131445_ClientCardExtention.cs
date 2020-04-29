namespace FamilyKitchen.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ClientCardExtention : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DeliveryPrice",
                table: "ClientCards",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Voucher",
                table: "ClientCards",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryPrice",
                table: "ClientCards");

            migrationBuilder.DropColumn(
                name: "Voucher",
                table: "ClientCards");
        }
    }
}
