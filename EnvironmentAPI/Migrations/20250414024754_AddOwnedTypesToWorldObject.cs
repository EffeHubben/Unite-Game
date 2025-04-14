using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnvironmentAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnedTypesToWorldObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorldObjects_Worlds_WorldId",
                table: "WorldObjects");

            migrationBuilder.RenameColumn(
                name: "ScaleZ",
                table: "WorldObjects",
                newName: "Scale_Z");

            migrationBuilder.RenameColumn(
                name: "ScaleY",
                table: "WorldObjects",
                newName: "Scale_Y");

            migrationBuilder.RenameColumn(
                name: "ScaleX",
                table: "WorldObjects",
                newName: "Scale_X");

            migrationBuilder.RenameColumn(
                name: "RotationZ",
                table: "WorldObjects",
                newName: "Rotation_Z");

            migrationBuilder.RenameColumn(
                name: "RotationY",
                table: "WorldObjects",
                newName: "Rotation_Y");

            migrationBuilder.RenameColumn(
                name: "RotationX",
                table: "WorldObjects",
                newName: "Rotation_X");

            migrationBuilder.RenameColumn(
                name: "PositionZ",
                table: "WorldObjects",
                newName: "Rotation_W");

            migrationBuilder.RenameColumn(
                name: "PositionY",
                table: "WorldObjects",
                newName: "Position_Z");

            migrationBuilder.RenameColumn(
                name: "PositionX",
                table: "WorldObjects",
                newName: "Position_Y");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Worlds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "WorldId",
                table: "WorldObjects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<float>(
                name: "Position_X",
                table: "WorldObjects",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddForeignKey(
                name: "FK_WorldObjects_Worlds_WorldId",
                table: "WorldObjects",
                column: "WorldId",
                principalTable: "Worlds",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorldObjects_Worlds_WorldId",
                table: "WorldObjects");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Worlds");

            migrationBuilder.DropColumn(
                name: "Position_X",
                table: "WorldObjects");

            migrationBuilder.RenameColumn(
                name: "Scale_Z",
                table: "WorldObjects",
                newName: "ScaleZ");

            migrationBuilder.RenameColumn(
                name: "Scale_Y",
                table: "WorldObjects",
                newName: "ScaleY");

            migrationBuilder.RenameColumn(
                name: "Scale_X",
                table: "WorldObjects",
                newName: "ScaleX");

            migrationBuilder.RenameColumn(
                name: "Rotation_Z",
                table: "WorldObjects",
                newName: "RotationZ");

            migrationBuilder.RenameColumn(
                name: "Rotation_Y",
                table: "WorldObjects",
                newName: "RotationY");

            migrationBuilder.RenameColumn(
                name: "Rotation_X",
                table: "WorldObjects",
                newName: "RotationX");

            migrationBuilder.RenameColumn(
                name: "Rotation_W",
                table: "WorldObjects",
                newName: "PositionZ");

            migrationBuilder.RenameColumn(
                name: "Position_Z",
                table: "WorldObjects",
                newName: "PositionY");

            migrationBuilder.RenameColumn(
                name: "Position_Y",
                table: "WorldObjects",
                newName: "PositionX");

            migrationBuilder.AlterColumn<int>(
                name: "WorldId",
                table: "WorldObjects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorldObjects_Worlds_WorldId",
                table: "WorldObjects",
                column: "WorldId",
                principalTable: "Worlds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
