using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.Dal.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTestAnswerEntityaddQuestionNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttemptsAnswers_QuestionAnswers_AnswerId",
                table: "AttemptsAnswers");

            migrationBuilder.RenameColumn(
                name: "AnswerId",
                table: "AttemptsAnswers",
                newName: "QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_AttemptsAnswers_AnswerId",
                table: "AttemptsAnswers",
                newName: "IX_AttemptsAnswers_QuestionId");

            migrationBuilder.AddColumn<int>(
                name: "QuestionWeight",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "AttemptsAnswers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "TestAnswerTestVariant",
                columns: table => new
                {
                    AnswerId = table.Column<long>(type: "bigint", nullable: false),
                    TestAnswerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestAnswerTestVariant", x => new { x.AnswerId, x.TestAnswerId });
                    table.ForeignKey(
                        name: "FK_TestAnswerTestVariant_AttemptsAnswers_TestAnswerId",
                        column: x => x.TestAnswerId,
                        principalTable: "AttemptsAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestAnswerTestVariant_QuestionAnswers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "QuestionAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestAnswerTestVariant_TestAnswerId",
                table: "TestAnswerTestVariant",
                column: "TestAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttemptsAnswers_Questions_QuestionId",
                table: "AttemptsAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttemptsAnswers_Questions_QuestionId",
                table: "AttemptsAnswers");

            migrationBuilder.DropTable(
                name: "TestAnswerTestVariant");

            migrationBuilder.DropColumn(
                name: "QuestionWeight",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "AttemptsAnswers");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "AttemptsAnswers",
                newName: "AnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_AttemptsAnswers_QuestionId",
                table: "AttemptsAnswers",
                newName: "IX_AttemptsAnswers_AnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttemptsAnswers_QuestionAnswers_AnswerId",
                table: "AttemptsAnswers",
                column: "AnswerId",
                principalTable: "QuestionAnswers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
