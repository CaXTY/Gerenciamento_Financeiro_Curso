using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GerenciamentoFinanceiroCurso.Migrations
{
    /// <inheritdoc />
    public partial class CriandoTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Transacoes",
                columns: table => new
                {
                    TransacaoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacoes", x => x.TransacaoId);
                });

            migrationBuilder.CreateTable(
                name: "Financas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: false),
                    DataOperacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoriaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TransacaoId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Financas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Financas_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Financas_Transacoes_TransacaoId",
                        column: x => x.TransacaoId,
                        principalTable: "Transacoes",
                        principalColumn: "TransacaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "CategoriaId", "Nome" },
                values: new object[,]
                {
                    { "comissao", "Comissão" },
                    { "educacao", "Educação" },
                    { "mercado", "Mercado" },
                    { "salario", "Salário" },
                    { "viagem", "Viagem" }
                });

            migrationBuilder.InsertData(
                table: "Transacoes",
                columns: new[] { "TransacaoId", "Nome" },
                values: new object[,]
                {
                    { "ganho", "Ganho" },
                    { "gasto", "Gasto" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Financas_CategoriaId",
                table: "Financas",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Financas_TransacaoId",
                table: "Financas",
                column: "TransacaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Financas");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Transacoes");
        }
    }
}
