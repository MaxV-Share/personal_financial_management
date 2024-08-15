using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinancialManagement.Models.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    amount = table.Column<double>(type: "float", nullable: true),
                    totalamount = table.Column<double>(name: "total_amount", type: "float", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    imageurl = table.Column<string>(name: "image_url", type: "nvarchar(max)", nullable: true),
                    transactiondate = table.Column<DateTime>(name: "transaction_date", type: "datetime2", nullable: true),
                    isreport = table.Column<bool>(name: "is_report", type: "bit", nullable: true),
                    fees = table.Column<double>(type: "float", nullable: true),
                    feescategoryid = table.Column<Guid>(name: "fees_category_id", type: "uniqueidentifier", nullable: true),
                    frompaymentaccountid = table.Column<Guid>(name: "from_payment_account_id", type: "uniqueidentifier", nullable: true),
                    topaymentaccountid = table.Column<Guid>(name: "to_payment_account_id", type: "uniqueidentifier", nullable: true),
                    categoryid = table.Column<Guid>(name: "category_id", type: "uniqueidentifier", nullable: true),
                    createat = table.Column<DateTime>(name: "create_at", type: "datetime2", nullable: true),
                    updateat = table.Column<DateTime>(name: "update_at", type: "datetime2", nullable: true),
                    createby = table.Column<string>(name: "create_by", type: "nvarchar(max)", nullable: true),
                    updateby = table.Column<string>(name: "update_by", type: "nvarchar(max)", nullable: true),
                    deleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transactions", x => x.id);
                    table.ForeignKey(
                        name: "fk_transactions_payment_accounts_from_payment_account_id",
                        column: x => x.frompaymentaccountid,
                        principalTable: "payment_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_transactions_payment_accounts_to_payment_account_id",
                        column: x => x.topaymentaccountid,
                        principalTable: "payment_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_transactions_transaction_categories_category_id",
                        column: x => x.categoryid,
                        principalTable: "transaction_categories",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_transactions_transaction_categories_fees_category_id",
                        column: x => x.feescategoryid,
                        principalTable: "transaction_categories",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_transactions_category_id",
                table: "transactions",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_transactions_fees_category_id",
                table: "transactions",
                column: "fees_category_id");

            migrationBuilder.CreateIndex(
                name: "ix_transactions_from_payment_account_id",
                table: "transactions",
                column: "from_payment_account_id");

            migrationBuilder.CreateIndex(
                name: "ix_transactions_id",
                table: "transactions",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_transactions_to_payment_account_id",
                table: "transactions",
                column: "to_payment_account_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transactions");
        }
    }
}
