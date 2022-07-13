using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace WebNotebook.Migrations
{
    public partial class AddAgendaCalendario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "agendaCalendarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    HoraIncio = table.Column<string>(type: "text", nullable: true),
                    HoraFim = table.Column<string>(type: "text", nullable: true),
                    Sunday = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Monday = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Tuesday = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Wednesday = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Thursday = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Friday = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Sartuday = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    QuantidadeDia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agendaCalendarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_agendaCalendarios_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_agendaCalendarios_ApplicationUserId",
                table: "agendaCalendarios",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agendaCalendarios");
        }
    }
}
