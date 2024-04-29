using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb3API.Migrations
{
    /// <inheritdoc />
    public partial class intiial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Humans",
                columns: table => new
                {
                    HumanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HumanName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HumanEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HumanNickname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HumanRandomQuote = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Humans", x => x.HumanId);
                });

            migrationBuilder.CreateTable(
                name: "Interests",
                columns: table => new
                {
                    InterestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterestTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterestDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interests", x => x.InterestId);
                });

            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    LinkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.LinkId);
                });

            migrationBuilder.CreateTable(
                name: "HumanInterests",
                columns: table => new
                {
                    HumanInterestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkHumanId = table.Column<int>(type: "int", nullable: false),
                    FkInterestId = table.Column<int>(type: "int", nullable: true),
                    FkLinkId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HumanInterests", x => x.HumanInterestId);
                    table.ForeignKey(
                        name: "FK_HumanInterests_Humans_FkHumanId",
                        column: x => x.FkHumanId,
                        principalTable: "Humans",
                        principalColumn: "HumanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HumanInterests_Interests_FkInterestId",
                        column: x => x.FkInterestId,
                        principalTable: "Interests",
                        principalColumn: "InterestId");
                    table.ForeignKey(
                        name: "FK_HumanInterests_Links_FkLinkId",
                        column: x => x.FkLinkId,
                        principalTable: "Links",
                        principalColumn: "LinkId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HumanInterests_FkHumanId",
                table: "HumanInterests",
                column: "FkHumanId");

            migrationBuilder.CreateIndex(
                name: "IX_HumanInterests_FkInterestId",
                table: "HumanInterests",
                column: "FkInterestId");

            migrationBuilder.CreateIndex(
                name: "IX_HumanInterests_FkLinkId",
                table: "HumanInterests",
                column: "FkLinkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HumanInterests");

            migrationBuilder.DropTable(
                name: "Humans");

            migrationBuilder.DropTable(
                name: "Interests");

            migrationBuilder.DropTable(
                name: "Links");
        }
    }
}
