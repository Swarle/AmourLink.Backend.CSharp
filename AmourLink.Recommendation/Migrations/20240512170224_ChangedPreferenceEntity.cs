using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmourLink.Recommendation.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPreferenceEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "age_range",
                table: "preference",
                newName: "min_age");

            migrationBuilder.AddColumn<int>(
                name: "max_age",
                table: "preference",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "max_age",
                table: "preference");

            migrationBuilder.RenameColumn(
                name: "min_age",
                table: "preference",
                newName: "age_range");
        }
    }
}
