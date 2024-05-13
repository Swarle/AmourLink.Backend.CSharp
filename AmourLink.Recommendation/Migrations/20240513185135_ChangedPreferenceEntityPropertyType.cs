using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmourLink.Recommendation.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPreferenceEntityPropertyType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "distance_range",
                table: "preference",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)",
                oldPrecision: 4,
                oldScale: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "distance_range",
                table: "preference",
                type: "decimal(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
