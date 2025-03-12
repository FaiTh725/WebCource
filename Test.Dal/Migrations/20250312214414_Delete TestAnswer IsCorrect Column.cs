using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.Dal.Migrations
{
    /// <inheritdoc />
    public partial class DeleteTestAnswerIsCorrectColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "AttemptsAnswers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "AttemptsAnswers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
