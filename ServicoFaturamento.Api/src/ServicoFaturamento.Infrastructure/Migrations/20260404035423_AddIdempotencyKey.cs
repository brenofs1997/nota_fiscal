using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicoFaturamento.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIdempotencyKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "idempotency_key",
                schema: "public",
                table: "nota_fiscal",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "uk_nota_fiscal_idempotency",
                schema: "public",
                table: "nota_fiscal",
                column: "idempotency_key",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "uk_nota_fiscal_idempotency",
                schema: "public",
                table: "nota_fiscal");

            migrationBuilder.DropColumn(
                name: "idempotency_key",
                schema: "public",
                table: "nota_fiscal");
        }
    }
}
