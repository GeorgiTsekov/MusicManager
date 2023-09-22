using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicManager.API.Migrations
{
    /// <inheritdoc />
    public partial class CreatingManagerclass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ManagerId",
                table: "Bands",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Money",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bands_ManagerId",
                table: "Bands",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bands_AspNetUsers_ManagerId",
                table: "Bands",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bands_AspNetUsers_ManagerId",
                table: "Bands");

            migrationBuilder.DropIndex(
                name: "IX_Bands_ManagerId",
                table: "Bands");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Bands");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Money",
                table: "AspNetUsers");
        }
    }
}
