using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace AmourLink.Recommendation.Migrations
{
    /// <inheritdoc />
    public partial class ChangedLocationPropertiesInUserDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_location_latitude",
                table: "user_details");

            migrationBuilder.DropColumn(
                name: "last_location_longitude",
                table: "user_details");

            migrationBuilder.AddColumn<Point>(
                name: "last_location",
                table: "user_details",
                type: "point",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_location",
                table: "user_details");

            migrationBuilder.AddColumn<float>(
                name: "last_location_latitude",
                table: "user_details",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "last_location_longitude",
                table: "user_details",
                type: "float",
                nullable: true);
        }
    }
}
