using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningSession.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLearningSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Learning");

            migrationBuilder.RenameTable(
                name: "LearningSessions",
                newName: "LearningSessions",
                newSchema: "Learning");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "LearningSessions",
                schema: "Learning",
                newName: "LearningSessions");
        }
    }
}
