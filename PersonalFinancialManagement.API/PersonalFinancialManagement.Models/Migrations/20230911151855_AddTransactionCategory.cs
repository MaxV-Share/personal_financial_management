using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinancialManagement.Models.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "transaction_categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    parentid = table.Column<Guid>(name: "parent_id", type: "uniqueidentifier", nullable: true),
                    typeid = table.Column<Guid>(name: "type_id", type: "uniqueidentifier", nullable: true),
                    createat = table.Column<DateTime>(name: "create_at", type: "datetime2", nullable: true),
                    updateat = table.Column<DateTime>(name: "update_at", type: "datetime2", nullable: true),
                    createby = table.Column<string>(name: "create_by", type: "nvarchar(max)", nullable: true),
                    updateby = table.Column<string>(name: "update_by", type: "nvarchar(max)", nullable: true),
                    deleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transaction_categories", x => x.id);
                    table.ForeignKey(
                        name: "fk_transaction_categories_transaction_categories_parent_id",
                        column: x => x.parentid,
                        principalTable: "transaction_categories",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_transaction_categories_transaction_category_types_type_id",
                        column: x => x.typeid,
                        principalTable: "transaction_category_types",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_transaction_categories_id",
                table: "transaction_categories",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_transaction_categories_parent_id",
                table: "transaction_categories",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "ix_transaction_categories_type_id",
                table: "transaction_categories",
                column: "type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transaction_categories");
        }
    }
}
