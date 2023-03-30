using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HatliFood.Migrations
{
    /// <inheritdoc />
    public partial class CategoryNavigationV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_Category_CidNavigationId",
                table: "MenuItem");

            migrationBuilder.DropIndex(
                name: "IX_MenuItem_CidNavigationId",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "CidNavigationId",
                table: "MenuItem");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_Cid",
                table: "MenuItem",
                column: "Cid");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_Category_Cid",
                table: "MenuItem",
                column: "Cid",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_Category_Cid",
                table: "MenuItem");

            migrationBuilder.DropIndex(
                name: "IX_MenuItem_Cid",
                table: "MenuItem");

            migrationBuilder.AddColumn<int>(
                name: "CidNavigationId",
                table: "MenuItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_CidNavigationId",
                table: "MenuItem",
                column: "CidNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_Category_CidNavigationId",
                table: "MenuItem",
                column: "CidNavigationId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
