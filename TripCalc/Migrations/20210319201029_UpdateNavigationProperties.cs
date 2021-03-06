using Microsoft.EntityFrameworkCore.Migrations;

namespace TripCalc.Migrations
{
    public partial class UpdateNavigationProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Expenses_ExpenseId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ExpenseId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "Students");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpenseId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_ExpenseId",
                table: "Students",
                column: "ExpenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Expenses_ExpenseId",
                table: "Students",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "ExpenseId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
