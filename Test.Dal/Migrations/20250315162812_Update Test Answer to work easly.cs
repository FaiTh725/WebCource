using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.Dal.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTestAnswertoworkeasly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestAnswerTestVariant_QuestionAnswers_AnswerId",
                table: "TestAnswerTestVariant");

            migrationBuilder.RenameColumn(
                name: "AnswerId",
                table: "TestAnswerTestVariant",
                newName: "AnswersId");

            migrationBuilder.AddColumn<string>(
                name: "AnswersId",
                table: "AttemptsAnswers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddForeignKey(
                name: "FK_TestAnswerTestVariant_QuestionAnswers_AnswersId",
                table: "TestAnswerTestVariant",
                column: "AnswersId",
                principalTable: "QuestionAnswers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestAnswerTestVariant_QuestionAnswers_AnswersId",
                table: "TestAnswerTestVariant");

            migrationBuilder.DropColumn(
                name: "AnswersId",
                table: "AttemptsAnswers");

            migrationBuilder.RenameColumn(
                name: "AnswersId",
                table: "TestAnswerTestVariant",
                newName: "AnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestAnswerTestVariant_QuestionAnswers_AnswerId",
                table: "TestAnswerTestVariant",
                column: "AnswerId",
                principalTable: "QuestionAnswers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
