using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace deskManagerApi.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "buildings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_buildings", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "floors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    BuildingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_floors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_floors_buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "buildings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Login = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    mapViewBox = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mapWidth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mapHeight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mapXmlns = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FloorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rooms_floors_FloorId",
                        column: x => x.FloorId,
                        principalTable: "floors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "desks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    MapXLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MapYLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Width = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomId = table.Column<int>(type: "int", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_desks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_desks_deskStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "deskStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_desks_rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "rooms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "deskTeams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeskId = table.Column<int>(type: "int", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deskTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_deskTeams_desks_DeskId",
                        column: x => x.DeskId,
                        principalTable: "desks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_deskTeams_teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "issues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeskId = table.Column<int>(type: "int", nullable: true),
                    ReporterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_issues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_issues_desks_DeskId",
                        column: x => x.DeskId,
                        principalTable: "desks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_issues_users_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    AddDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    DeskId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_items_brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "brands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_items_desks_DeskId",
                        column: x => x.DeskId,
                        principalTable: "desks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_items_users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "issueHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusFrom = table.Column<int>(type: "int", nullable: false),
                    StatusTo = table.Column<int>(type: "int", nullable: false),
                    ChangedBy = table.Column<int>(type: "int", nullable: true),
                    IssueId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_issueHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_issueHistories_issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "issues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_issueHistories_users_ChangedBy",
                        column: x => x.ChangedBy,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_desks_RoomId",
                table: "desks",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_desks_StatusId",
                table: "desks",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_deskTeams_DeskId",
                table: "deskTeams",
                column: "DeskId");

            migrationBuilder.CreateIndex(
                name: "IX_deskTeams_TeamId",
                table: "deskTeams",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_floors_BuildingId",
                table: "floors",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_issueHistories_ChangedBy",
                table: "issueHistories",
                column: "ChangedBy");

            migrationBuilder.CreateIndex(
                name: "IX_issueHistories_IssueId",
                table: "issueHistories",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_issues_DeskId",
                table: "issues",
                column: "DeskId");

            migrationBuilder.CreateIndex(
                name: "IX_issues_ReporterId",
                table: "issues",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_items_BrandId",
                table: "items",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_items_DeskId",
                table: "items",
                column: "DeskId");

            migrationBuilder.CreateIndex(
                name: "IX_items_OwnerId",
                table: "items",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_rooms_FloorId",
                table: "rooms",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_users_TeamId",
                table: "users",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deskTeams");

            migrationBuilder.DropTable(
                name: "issueHistories");

            migrationBuilder.DropTable(
                name: "items");

            migrationBuilder.DropTable(
                name: "issues");

            migrationBuilder.DropTable(
                name: "brands");

            migrationBuilder.DropTable(
                name: "desks");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "deskStatuses");

            migrationBuilder.DropTable(
                name: "rooms");

            migrationBuilder.DropTable(
                name: "teams");

            migrationBuilder.DropTable(
                name: "floors");

            migrationBuilder.DropTable(
                name: "buildings");
        }
    }
}
