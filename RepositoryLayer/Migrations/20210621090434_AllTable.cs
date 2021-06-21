using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class AllTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Label_Labels_LabelId",
                table: "User_Label");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Label_Users_UserId",
                table: "User_Label");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User_Label",
                table: "User_Label");

            migrationBuilder.RenameTable(
                name: "User_Label",
                newName: "User_Labels");

            migrationBuilder.RenameIndex(
                name: "IX_User_Label_UserId",
                table: "User_Labels",
                newName: "IX_User_Labels_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_User_Label_LabelId",
                table: "User_Labels",
                newName: "IX_User_Labels_LabelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_Labels",
                table: "User_Labels",
                column: "User_Label_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Labels_Labels_LabelId",
                table: "User_Labels",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "LabelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Labels_Users_UserId",
                table: "User_Labels",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Labels_Labels_LabelId",
                table: "User_Labels");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Labels_Users_UserId",
                table: "User_Labels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User_Labels",
                table: "User_Labels");

            migrationBuilder.RenameTable(
                name: "User_Labels",
                newName: "User_Label");

            migrationBuilder.RenameIndex(
                name: "IX_User_Labels_UserId",
                table: "User_Label",
                newName: "IX_User_Label_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_User_Labels_LabelId",
                table: "User_Label",
                newName: "IX_User_Label_LabelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_Label",
                table: "User_Label",
                column: "User_Label_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Label_Labels_LabelId",
                table: "User_Label",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "LabelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Label_Users_UserId",
                table: "User_Label",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
