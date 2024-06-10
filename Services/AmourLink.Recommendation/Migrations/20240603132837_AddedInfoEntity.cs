using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmourLink.Recommendation.Migrations
{
    /// <inheritdoc />
    public partial class AddedInfoEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "info",
                columns: table => new
                {
                    info_id = table.Column<byte[]>(type: "binary(16)", nullable: false),
                    title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.info_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "info_answer",
                columns: table => new
                {
                    info_answer_id = table.Column<byte[]>(type: "binary(16)", nullable: false),
                    answer = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    info_id = table.Column<byte[]>(type: "binary(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.info_answer_id);
                    table.ForeignKey(
                        name: "fk_info_answer_info_info_id",
                        column: x => x.info_id,
                        principalTable: "info",
                        principalColumn: "info_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "info_details",
                columns: table => new
                {
                    info_id = table.Column<byte[]>(type: "binary(16)", nullable: false),
                    user_id = table.Column<byte[]>(type: "binary(16)", nullable: false),
                    info_answer_id = table.Column<byte[]>(type: "binary(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.info_id, x.info_answer_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_info_details_info_answer_info_answer_id",
                        column: x => x.info_answer_id,
                        principalTable: "info_answer",
                        principalColumn: "info_answer_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_info_details_info_info_id",
                        column: x => x.info_id,
                        principalTable: "info",
                        principalColumn: "info_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_info_details_user_details_user_id",
                        column: x => x.user_id,
                        principalTable: "user_details",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_info_answer_info_id",
                table: "info_answer",
                column: "info_id");

            migrationBuilder.CreateIndex(
                name: "ix_info_details_info_answer_id",
                table: "info_details",
                column: "info_answer_id");

            migrationBuilder.CreateIndex(
                name: "ix_info_details_user_id",
                table: "info_details",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "info_details");

            migrationBuilder.DropTable(
                name: "info_answer");

            migrationBuilder.DropTable(
                name: "info");
        }
    }
}
