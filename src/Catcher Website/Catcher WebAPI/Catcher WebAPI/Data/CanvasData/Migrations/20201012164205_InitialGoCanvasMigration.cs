using Microsoft.EntityFrameworkCore.Migrations;

namespace Catcher_WebAPI.Data.CanvasData.Migrations
{
    public partial class InitialGoCanvasMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblEmployees",
                columns: table => new
                {
                    LineID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BadgeID = table.Column<int>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Suspended = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblEmployees", x => x.LineID);
                });

            migrationBuilder.CreateTable(
                name: "tblLocations",
                columns: table => new
                {
                    Location = table.Column<string>(maxLength: 50, nullable: true),
                    FormName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tblMySubmissions",
                columns: table => new
                {
                    LineID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoCanSubmissionGUID = table.Column<string>(maxLength: 50, nullable: false),
                    FormName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMySubmissions", x => x.LineID);
                });

            migrationBuilder.CreateTable(
                name: "tblSystemStatus",
                columns: table => new
                {
                    LineID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSystemStatus", x => x.LineID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblEmployees");

            migrationBuilder.DropTable(
                name: "tblLocations");

            migrationBuilder.DropTable(
                name: "tblMySubmissions");

            migrationBuilder.DropTable(
                name: "tblSystemStatus");
        }
    }
}
