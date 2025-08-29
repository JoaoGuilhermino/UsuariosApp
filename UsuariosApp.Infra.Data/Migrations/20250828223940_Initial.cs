using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsuariosApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PERFIL",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERFIL", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PERMISSAO",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SERVICO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TIPO = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERMISSAO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SENHA = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PERFIL_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USUARIO_PERFIL_PERFIL_ID",
                        column: x => x.PERFIL_ID,
                        principalTable: "PERFIL",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PERFIL_PERMISSAO",
                columns: table => new
                {
                    PERFIL_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PERMISSAO_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERFIL_PERMISSAO", x => new { x.PERFIL_ID, x.PERMISSAO_ID });
                    table.ForeignKey(
                        name: "FK_PERFIL_PERMISSAO_PERFIL_PERFIL_ID",
                        column: x => x.PERFIL_ID,
                        principalTable: "PERFIL",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PERFIL_PERMISSAO_PERMISSAO_PERMISSAO_ID",
                        column: x => x.PERMISSAO_ID,
                        principalTable: "PERMISSAO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PERFIL_NOME",
                table: "PERFIL",
                column: "NOME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PERFIL_PERMISSAO_PERMISSAO_ID",
                table: "PERFIL_PERMISSAO",
                column: "PERMISSAO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PERMISSAO_NOME",
                table: "PERMISSAO",
                column: "NOME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_EMAIL",
                table: "USUARIO",
                column: "EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_PERFIL_ID",
                table: "USUARIO",
                column: "PERFIL_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PERFIL_PERMISSAO");

            migrationBuilder.DropTable(
                name: "USUARIO");

            migrationBuilder.DropTable(
                name: "PERMISSAO");

            migrationBuilder.DropTable(
                name: "PERFIL");
        }
    }
}
