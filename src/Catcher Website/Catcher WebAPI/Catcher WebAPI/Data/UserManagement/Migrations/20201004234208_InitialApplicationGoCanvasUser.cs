using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Catcher_WebAPI.Data.UserManagement.Migrations
{
    public partial class InitialApplicationGoCanvasUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "UserManagement");

            migrationBuilder.CreateTable(
                name: "tblUser",
                schema: "UserManagement",
                columns: table => new
                {
                    UserID = table.Column<string>(maxLength: 450, nullable: false),
                    PWD = table.Column<string>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false, defaultValue: false),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false, defaultValue: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false, defaultValue: false),
                    LockoutEnabled = table.Column<bool>(nullable: false, defaultValue: false),
                    AccessFailedCount = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserId", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "tblUserRole",
                schema: "UserManagement",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblUserClaim",
                schema: "UserManagement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblUserClaim_tblUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "UserManagement",
                        principalTable: "tblUser",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblUserLogin",
                schema: "UserManagement",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 50, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 50, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_tblUserLogin_tblUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "UserManagement",
                        principalTable: "tblUser",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblUserToken",
                schema: "UserManagement",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_tblUserToken_tblUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "UserManagement",
                        principalTable: "tblUser",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblUserRoleClaim",
                schema: "UserManagement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserRoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblUserRoleClaim_tblUserRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "UserManagement",
                        principalTable: "tblUserRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblUserUsersRole",
                schema: "UserManagement",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserUsersRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_tblUserUsersRole_tblUserRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "UserManagement",
                        principalTable: "tblUserRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblUserUsersRole_tblUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "UserManagement",
                        principalTable: "tblUser",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "UserManagement",
                table: "tblUser",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "UserManagement",
                table: "tblUser",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tblUserClaim_UserId",
                schema: "UserManagement",
                table: "tblUserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblUserLogin_UserId",
                schema: "UserManagement",
                table: "tblUserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "UserManagement",
                table: "tblUserRole",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tblUserRoleClaim_RoleId",
                schema: "UserManagement",
                table: "tblUserRoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tblUserUsersRole_RoleId",
                schema: "UserManagement",
                table: "tblUserUsersRole",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblUserClaim",
                schema: "UserManagement");

            migrationBuilder.DropTable(
                name: "tblUserLogin",
                schema: "UserManagement");

            migrationBuilder.DropTable(
                name: "tblUserRoleClaim",
                schema: "UserManagement");

            migrationBuilder.DropTable(
                name: "tblUserToken",
                schema: "UserManagement");

            migrationBuilder.DropTable(
                name: "tblUserUsersRole",
                schema: "UserManagement");

            migrationBuilder.DropTable(
                name: "tblUserRole",
                schema: "UserManagement");

            migrationBuilder.DropTable(
                name: "tblUser",
                schema: "UserManagement");
        }
    }
}
