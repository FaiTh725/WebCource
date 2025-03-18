using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddTestdurationwhenopentest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestDuration",
                table: "TestAccesses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddCheckConstraint(
                name: "CK_TestAccesses_TestDuration",
                table: "TestAccesses",
                sql: "TestDuration > 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_TestAccesses_TestDuration",
                table: "TestAccesses");

            migrationBuilder.DropColumn(
                name: "TestDuration",
                table: "TestAccesses");
        }
    }
}
