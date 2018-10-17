using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Infrastructure.Migrations
{
    public partial class V001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    RowFlag = table.Column<byte[]>(rowVersion: true, nullable: true),
                    LastChange = table.Column<DateTime>(nullable: false),
                    Memo = table.Column<string>(maxLength: 100, nullable: true),
                    No = table.Column<string>(maxLength: 8, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    RowFlag = table.Column<byte[]>(rowVersion: true, nullable: true),
                    LastChange = table.Column<DateTime>(nullable: false),
                    Memo = table.Column<string>(maxLength: 100, nullable: true),
                    WorkNo = table.Column<string>(maxLength: 8, nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    MobileNo = table.Column<string>(maxLength: 20, nullable: true),
                    BusinessId = table.Column<Guid>(nullable: false),
                    Permission = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_BusinessId",
                table: "Users",
                column: "BusinessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Businesses");
        }
    }
}
