using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _01.CodeFirst.Migrations
{
    public partial class newDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KeyCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CardNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BedCount = table.Column<int>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    IsAvailable = table.Column<bool>(nullable: false),
                    RoomType = table.Column<int>(nullable: false),
                    Cost = table.Column<decimal>(nullable: true),
                    RoomNickName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomsKeyCards",
                columns: table => new
                {
                    RoomId = table.Column<int>(nullable: false),
                    KeyCardId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomsKeyCards", x => new { x.RoomId, x.KeyCardId });
                    table.ForeignKey(
                        name: "FK_RoomsKeyCards_KeyCards_KeyCardId",
                        column: x => x.KeyCardId,
                        principalTable: "KeyCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomsKeyCards_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomsKeyCards_KeyCardId",
                table: "RoomsKeyCards",
                column: "KeyCardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomsKeyCards");

            migrationBuilder.DropTable(
                name: "KeyCards");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
