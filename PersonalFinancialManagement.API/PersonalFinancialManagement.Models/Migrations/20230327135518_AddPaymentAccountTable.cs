using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinancialManagement.Models.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentAccountTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "payment_accounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    initialmoney = table.Column<string>(name: "initial_money", type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isreport = table.Column<bool>(name: "is_report", type: "bit", nullable: true),
                    icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    currencyid = table.Column<Guid>(name: "currency_id", type: "uniqueidentifier", nullable: true),
                    typeid = table.Column<Guid>(name: "type_id", type: "uniqueidentifier", nullable: true),
                    createat = table.Column<DateTime>(name: "create_at", type: "datetime2", nullable: true),
                    updateat = table.Column<DateTime>(name: "update_at", type: "datetime2", nullable: true),
                    createby = table.Column<string>(name: "create_by", type: "nvarchar(max)", nullable: true),
                    updateby = table.Column<string>(name: "update_by", type: "nvarchar(max)", nullable: true),
                    deleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_accounts", x => x.id);
                    table.ForeignKey(
                        name: "fk_payment_accounts_currencies_currency_id",
                        column: x => x.currencyid,
                        principalTable: "currencies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_payment_accounts_payment_account_types_type_id",
                        column: x => x.typeid,
                        principalTable: "payment_account_types",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_payment_accounts_currency_id",
                table: "payment_accounts",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "ix_payment_accounts_id",
                table: "payment_accounts",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_payment_accounts_type_id",
                table: "payment_accounts",
                column: "type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payment_accounts");
        }
    }
}
