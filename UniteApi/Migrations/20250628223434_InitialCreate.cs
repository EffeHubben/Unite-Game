using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniteApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Worlds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slot = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worlds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorldObjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrefabName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position_X = table.Column<float>(type: "real", nullable: false),
                    Position_Y = table.Column<float>(type: "real", nullable: false),
                    Position_Z = table.Column<float>(type: "real", nullable: false),
                    Scale_X = table.Column<float>(type: "real", nullable: false),
                    Scale_Y = table.Column<float>(type: "real", nullable: false),
                    Scale_Z = table.Column<float>(type: "real", nullable: false),
                    Rotation_X = table.Column<float>(type: "real", nullable: false),
                    Rotation_Y = table.Column<float>(type: "real", nullable: false),
                    Rotation_Z = table.Column<float>(type: "real", nullable: false),
                    Rotation_W = table.Column<float>(type: "real", nullable: false),
                    WorldEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorldObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorldObjects_Worlds_WorldEntityId",
                        column: x => x.WorldEntityId,
                        principalTable: "Worlds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorldObjects_WorldEntityId",
                table: "WorldObjects",
                column: "WorldEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorldObjects");

            migrationBuilder.DropTable(
                name: "Worlds");
        }
    }
}
