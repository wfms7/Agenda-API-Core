using Microsoft.EntityFrameworkCore.Migrations;

namespace WebNotebook.Migrations
{
    public partial class addcampoemailnopaciente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Paciente",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Paciente");
        }
    }
}
