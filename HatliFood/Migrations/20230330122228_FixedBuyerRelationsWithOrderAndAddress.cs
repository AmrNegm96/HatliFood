using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HatliFood.Migrations
{
    /// <inheritdoc />
    public partial class FixedBuyerRelationsWithOrderAndAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DID",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "RID",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "buyerUserId",
                table: "Order");

            migrationBuilder.AlterColumn<string>(
                name: "BID",
                table: "Order",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "BID",
                table: "Addresss",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Order_BID",
                table: "Order",
                column: "BID");

            migrationBuilder.CreateIndex(
                name: "IX_Addresss_BID",
                table: "Addresss",
                column: "BID");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresss_Buyers_BID",
                table: "Addresss",
                column: "BID",
                principalTable: "Buyers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Buyers_BID",
                table: "Order",
                column: "BID",
                principalTable: "Buyers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresss_Buyers_BID",
                table: "Addresss");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Buyers_BID",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_BID",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Addresss_BID",
                table: "Addresss");

            migrationBuilder.AlterColumn<int>(
                name: "BID",
                table: "Order",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "DID",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RID",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "buyerUserId",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "BID",
                table: "Addresss",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
