using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ServicoFaturamento.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjustTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "nota_fiscal",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    numero_sequencial = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status = table.Column<string>(type: "text", nullable: false),
                    data_emissao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("nota_fiscal_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "itens_nota_fiscal",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    nota_fiscal_id = table.Column<Guid>(type: "uuid", nullable: false),
                    produto_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantidade = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("itens_nota_fiscal_pkey", x => x.id);
                    table.ForeignKey(
                        name: "FK_itens_nota_fiscal_nota_fiscal_nota_fiscal_id",
                        column: x => x.nota_fiscal_id,
                        principalSchema: "public",
                        principalTable: "nota_fiscal",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_itens_nota_fiscal_nota_fiscal_id",
                schema: "public",
                table: "itens_nota_fiscal",
                column: "nota_fiscal_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "itens_nota_fiscal",
                schema: "public");

            migrationBuilder.DropTable(
                name: "nota_fiscal",
                schema: "public");
        }
    }
}
