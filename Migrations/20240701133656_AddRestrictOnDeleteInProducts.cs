using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShop.Migrations
{
    /// <inheritdoc />
    public partial class AddRestrictOnDeleteInProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_AnimalCategory_AnimalCategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_ProductCategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Make_MakeId",
                table: "Product");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_AnimalCategory_AnimalCategoryId",
                table: "Product",
                column: "AnimalCategoryId",
                principalTable: "AnimalCategory",
                principalColumn: "AnimalCategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_ProductCategoryId",
                table: "Product",
                column: "ProductCategoryId",
                principalTable: "Category",
                principalColumn: "ProductCategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Make_MakeId",
                table: "Product",
                column: "MakeId",
                principalTable: "Make",
                principalColumn: "MakeId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_AnimalCategory_AnimalCategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_ProductCategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Make_MakeId",
                table: "Product");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_AnimalCategory_AnimalCategoryId",
                table: "Product",
                column: "AnimalCategoryId",
                principalTable: "AnimalCategory",
                principalColumn: "AnimalCategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_ProductCategoryId",
                table: "Product",
                column: "ProductCategoryId",
                principalTable: "Category",
                principalColumn: "ProductCategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Make_MakeId",
                table: "Product",
                column: "MakeId",
                principalTable: "Make",
                principalColumn: "MakeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
