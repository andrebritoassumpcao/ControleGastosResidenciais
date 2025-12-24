using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleGastosResidenciais.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexToPersonName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transactions_categories_categoryId",
                table: "transactions");

            migrationBuilder.RenameColumn(
                name: "categoryId",
                table: "transactions",
                newName: "category_id");

            migrationBuilder.RenameIndex(
                name: "IX_transactions_categoryId",
                table: "transactions",
                newName: "IX_transactions_category_id");

            migrationBuilder.AlterColumn<decimal>(
                name: "value",
                table: "transactions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_persons_name",
                table: "persons",
                column: "name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_categories_category_id",
                table: "transactions",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transactions_categories_category_id",
                table: "transactions");

            migrationBuilder.DropIndex(
                name: "IX_persons_name",
                table: "persons");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "transactions",
                newName: "categoryId");

            migrationBuilder.RenameIndex(
                name: "IX_transactions_category_id",
                table: "transactions",
                newName: "IX_transactions_categoryId");

            migrationBuilder.AlterColumn<decimal>(
                name: "value",
                table: "transactions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_categories_categoryId",
                table: "transactions",
                column: "categoryId",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
