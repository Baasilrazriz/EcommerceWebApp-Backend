using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class OrdersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "OrderModels",
                newName: "IdUser");

            migrationBuilder.AddColumn<int>(
                name: "IdProduct",
                table: "OrderModels",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdProduct",
                table: "OrderModels");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "OrderModels",
                newName: "UserID");
        }
    }
}
