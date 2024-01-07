using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class RestaurantCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdProduct",
                table: "OrderModels",
                newName: "ProdID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProdID",
                table: "OrderModels",
                newName: "IdProduct");
        }
    }
}
