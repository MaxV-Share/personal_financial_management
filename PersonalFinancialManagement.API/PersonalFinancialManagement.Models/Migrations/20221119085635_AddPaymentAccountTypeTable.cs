using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinancialManagement.Models.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentAccountTypeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "payment_account_types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createat = table.Column<DateTime>(name: "create_at", type: "datetime2", nullable: true),
                    updateat = table.Column<DateTime>(name: "update_at", type: "datetime2", nullable: true),
                    createby = table.Column<string>(name: "create_by", type: "nvarchar(max)", nullable: true),
                    updateby = table.Column<string>(name: "update_by", type: "nvarchar(max)", nullable: true),
                    deleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_account_types", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_payment_account_types_id",
                table: "payment_account_types",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payment_account_types");
        }
    }
}
