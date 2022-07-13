using Microsoft.EntityFrameworkCore.Migrations;

namespace WebNotebook.Migrations
{
    public partial class NomeAgendaadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NomeAgenda",
                table: "agendaCalendarios",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeAgenda",
                table: "agendaCalendarios");
        }
    }
}
