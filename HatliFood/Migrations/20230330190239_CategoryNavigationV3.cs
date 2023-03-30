using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HatliFood.Migrations
{
    /// <inheritdoc />
    public partial class CategoryNavigationV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Restaurant_RID",
                table: "Category");

            migrationBuilder.RenameColumn(
                name: "RID",
                table: "Category",
                newName: "Rid");

            migrationBuilder.RenameIndex(
                name: "IX_Category_RID",
                table: "Category",
                newName: "IX_Category_Rid");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Restaurant_Rid",
                table: "Category",
                column: "Rid",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Restaurant_Rid",
                table: "Category");

            migrationBuilder.RenameColumn(
                name: "Rid",
                table: "Category",
                newName: "RID");

            migrationBuilder.RenameIndex(
                name: "IX_Category_Rid",
                table: "Category",
                newName: "IX_Category_RID");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Restaurant_RID",
                table: "Category",
                column: "RID",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
