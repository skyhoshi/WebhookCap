using Microsoft.EntityFrameworkCore.Migrations;

namespace Catcher_WebAPI.Data.CanvasLogging.Migrations
{
    public partial class InitialGoCanvasMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblResponseLog",
                columns: table => new
                {
                    LineID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResponseBody = table.Column<string>(nullable: false),
                    TestRecord = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblResponseLog", x => x.LineID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblResponseLog");
        }
    }
}
