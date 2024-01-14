using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace deskManagerApi.Migrations
{
    public partial class DeskStatusChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_desks_deskStatuses_StatusId",
                table: "desks");

            migrationBuilder.DropTable(
                name: "deskStatuses");

            migrationBuilder.DropIndex(
                name: "IX_desks_StatusId",
                table: "desks");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "desks");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "desks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "desks");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "desks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "deskStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deskStatuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_desks_StatusId",
                table: "desks",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_desks_deskStatuses_StatusId",
                table: "desks",
                column: "StatusId",
                principalTable: "deskStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
