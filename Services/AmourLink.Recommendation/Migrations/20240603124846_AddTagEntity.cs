using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmourLink.Recommendation.Migrations
{
    /// <inheritdoc />
    public partial class AddTagEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "added_time",
                table: "picture");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "user",
                newName: "password");

            migrationBuilder.AddColumn<int>(
                name: "position",
                table: "picture",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "time_added",
                table: "picture",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "tag",
                columns: table => new
                {
                    tag_id = table.Column<byte[]>(type: "binary(16)", nullable: false),
                    tag_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.tag_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_details_tag",
                columns: table => new
                {
                    tag_id = table.Column<byte[]>(type: "binary(16)", nullable: false),
                    user_id = table.Column<byte[]>(type: "binary(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_details_tag", x => new { x.tag_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_user_details_tag_tag_tag_id",
                        column: x => x.tag_id,
                        principalTable: "tag",
                        principalColumn: "tag_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_details_tag_user_details_user_id",
                        column: x => x.user_id,
                        principalTable: "user_details",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_user_details_tag_user_id",
                table: "user_details_tag",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_details_tag");

            migrationBuilder.DropTable(
                name: "tag");

            migrationBuilder.DropColumn(
                name: "position",
                table: "picture");

            migrationBuilder.DropColumn(
                name: "time_added",
                table: "picture");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "user",
                newName: "password_hash");

            migrationBuilder.AddColumn<DateTime>(
                name: "added_time",
                table: "picture",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
