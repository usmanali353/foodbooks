using Microsoft.EntityFrameworkCore.Migrations;

namespace foodbooks.Migrations
{
    public partial class thirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Questions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionOptionId",
                table: "CustomerFeedBacks",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerFeedBacks_QuestionOptions_questionOptionsQuestionOptionId",
                table: "CustomerFeedBacks");

            migrationBuilder.DropIndex(
                name: "IX_CustomerFeedBacks_questionOptionsQuestionOptionId",
                table: "CustomerFeedBacks");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "QuestionOptionId",
                table: "CustomerFeedBacks");

            migrationBuilder.DropColumn(
                name: "questionOptionsQuestionOptionId",
                table: "CustomerFeedBacks");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
