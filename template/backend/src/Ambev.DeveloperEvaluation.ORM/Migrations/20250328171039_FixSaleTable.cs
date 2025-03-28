using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class FixSaleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "sale_seq");

            migrationBuilder.AlterColumn<long>(
                name: "SaleNumber",
                table: "Sale",
                type: "bigint",
                nullable: false,
                defaultValueSql: "nextval('sale_seq')",
                oldClrType: typeof(long),
                oldType: "bigserial");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "SaleDate",
                table: "Sale",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "sale_seq");

            migrationBuilder.AlterColumn<long>(
                name: "SaleNumber",
                table: "Sale",
                type: "bigserial",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValueSql: "nextval('sale_seq')");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SaleDate",
                table: "Sale",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
