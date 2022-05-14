using Microsoft.EntityFrameworkCore.Migrations;

namespace WebNotebook.Migrations
{
    public partial class JuntandoEspecialidadeeUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EspecialidadeDRs",
                columns: table => new
                {
                    ApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    EspecialidadeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspecialidadeDRs", x => new { x.ApplicationUserId, x.EspecialidadeId });
                    table.ForeignKey(
                        name: "FK_EspecialidadeDRs_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EspecialidadeDRs_Especialidade_EspecialidadeId",
                        column: x => x.EspecialidadeId,
                        principalTable: "Especialidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EspecialidadeDRs_EspecialidadeId",
                table: "EspecialidadeDRs",
                column: "EspecialidadeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EspecialidadeDRs");
        }
    }
}
