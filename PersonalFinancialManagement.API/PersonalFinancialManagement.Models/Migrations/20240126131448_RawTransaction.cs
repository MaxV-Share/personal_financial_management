using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinancialManagement.Models.Migrations
{
    /// <inheritdoc />
    public partial class RawTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "raw_transactions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mail_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    transaction_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amount = table.Column<double>(type: "float", nullable: false),
                    transaction_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    wallet_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    reference_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    balance = table.Column<double>(type: "float", nullable: false),
                    raw_string = table.Column<double>(type: "float", nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    create_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    update_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    deleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_raw_transactions", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_raw_transactions_id",
                table: "raw_transactions",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "raw_transactions");
        }
    }
}
