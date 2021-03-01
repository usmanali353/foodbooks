using Microsoft.EntityFrameworkCore.Migrations;

namespace foodbooks.Migrations
{
    public partial class fourthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerFeedBacks_QuestionOptions_questionOptionsQuestionOptionId",
                table: "CustomerFeedBacks");

            migrationBuilder.DropIndex(
                name: "IX_CustomerFeedBacks_questionOptionsQuestionOptionId",
                table: "CustomerFeedBacks");

            migrationBuilder.DropColumn(
                name: "questionOptionsQuestionOptionId",
                table: "CustomerFeedBacks");

            migrationBuilder.RenameColumn(
                name: "QuestionOptionId",
                table: "CustomerFeedBacks",
                newName: "QuestionOptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerFeedBacks_QuestionOptionsId",
                table: "CustomerFeedBacks",
                column: "QuestionOptionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerFeedBacks_QuestionOptions_QuestionOptionsId",
                table: "CustomerFeedBacks",
                column: "QuestionOptionsId",
                principalTable: "QuestionOptions",
                principalColumn: "QuestionOptionId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerFeedBacks_QuestionOptions_QuestionOptionsId",
                table: "CustomerFeedBacks");

            migrationBuilder.DropIndex(
                name: "IX_CustomerFeedBacks_QuestionOptionsId",
                table: "CustomerFeedBacks");

            migrationBuilder.RenameColumn(
                name: "QuestionOptionsId",
                table: "CustomerFeedBacks",
                newName: "QuestionOptionId");

            migrationBuilder.AddColumn<int>(
                name: "questionOptionsQuestionOptionId",
                table: "CustomerFeedBacks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerFeedBacks_questionOptionsQuestionOptionId",
                table: "CustomerFeedBacks",
                column: "questionOptionsQuestionOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerFeedBacks_QuestionOptions_questionOptionsQuestionOptionId",
                table: "CustomerFeedBacks",
                column: "questionOptionsQuestionOptionId",
                principalTable: "QuestionOptions",
                principalColumn: "QuestionOptionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
