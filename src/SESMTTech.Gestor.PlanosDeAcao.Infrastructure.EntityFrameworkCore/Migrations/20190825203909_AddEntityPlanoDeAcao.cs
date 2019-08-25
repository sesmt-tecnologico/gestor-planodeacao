using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SESMTTech.Gestor.PlanosDeAcao.Infrastructure.EntityFrameworkCore.Migrations
{
    public partial class AddEntityPlanoDeAcao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanosDeAcao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreationUser = table.Column<string>(maxLength: 100, nullable: true),
                    Codigo = table.Column<string>(maxLength: 100, nullable: false),
                    Ano = table.Column<int>(nullable: false),
                    Numero = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanosDeAcao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Itens",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreationUser = table.Column<string>(maxLength: 100, nullable: true),
                    Numero = table.Column<int>(nullable: false),
                    Codigo = table.Column<string>(maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    Acao = table.Column<string>(nullable: true),
                    Prazo = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DataRealizacao = table.Column<DateTime>(nullable: true),
                    PlanoDeAcaoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Itens_PlanosDeAcao_PlanoDeAcaoId",
                        column: x => x.PlanoDeAcaoId,
                        principalTable: "PlanosDeAcao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Responsaveis",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreationUser = table.Column<string>(maxLength: 100, nullable: true),
                    NomeCompleto = table.Column<string>(maxLength: 200, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    ItemId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsaveis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Responsaveis_Itens_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Itens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Itens_Codigo",
                table: "Itens",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Itens_PlanoDeAcaoId",
                table: "Itens",
                column: "PlanoDeAcaoId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanosDeAcao_Ano",
                table: "PlanosDeAcao",
                column: "Ano");

            migrationBuilder.CreateIndex(
                name: "IX_PlanosDeAcao_Codigo",
                table: "PlanosDeAcao",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Responsaveis_ItemId",
                table: "Responsaveis",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Responsaveis");

            migrationBuilder.DropTable(
                name: "Itens");

            migrationBuilder.DropTable(
                name: "PlanosDeAcao");
        }
    }
}
