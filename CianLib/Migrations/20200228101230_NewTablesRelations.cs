using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CianLib.Migrations
{
    public partial class NewTablesRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CianObjectPrices_CianObjects_cian_objectrow_id",
                table: "CianObjectPrices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CianObjects",
                table: "CianObjects");

            migrationBuilder.DropIndex(
                name: "IX_CianObjectPrices_cian_objectrow_id",
                table: "CianObjectPrices");

            migrationBuilder.DropColumn(
                name: "row_id",
                table: "CianObjects");

            migrationBuilder.DropColumn(
                name: "insert_date",
                table: "CianObjects");

            migrationBuilder.DropColumn(
                name: "cian_objectrow_id",
                table: "CianObjectPrices");

            migrationBuilder.AddColumn<bool>(
                name: "soft_deleted",
                table: "Offers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "soft_deleted",
                table: "CianObjects",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "cian_id",
                table: "CianObjectPrices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "city",
                table: "CianObjectPrices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CianObjects",
                table: "CianObjects",
                columns: new[] { "cian_id", "city" });

            migrationBuilder.CreateIndex(
                name: "IX_CianObjectPrices_cian_id_city",
                table: "CianObjectPrices",
                columns: new[] { "cian_id", "city" });

            migrationBuilder.AddForeignKey(
                name: "FK_CianObjectPrices_CianObjects_cian_id_city",
                table: "CianObjectPrices",
                columns: new[] { "cian_id", "city" },
                principalTable: "CianObjects",
                principalColumns: new[] { "cian_id", "city" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CianObjectPrices_CianObjects_cian_id_city",
                table: "CianObjectPrices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CianObjects",
                table: "CianObjects");

            migrationBuilder.DropIndex(
                name: "IX_CianObjectPrices_cian_id_city",
                table: "CianObjectPrices");

            migrationBuilder.DropColumn(
                name: "soft_deleted",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "soft_deleted",
                table: "CianObjects");

            migrationBuilder.DropColumn(
                name: "cian_id",
                table: "CianObjectPrices");

            migrationBuilder.DropColumn(
                name: "city",
                table: "CianObjectPrices");

            migrationBuilder.AddColumn<long>(
                name: "row_id",
                table: "CianObjects",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "insert_date",
                table: "CianObjects",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "cian_objectrow_id",
                table: "CianObjectPrices",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CianObjects",
                table: "CianObjects",
                column: "row_id");

            migrationBuilder.CreateIndex(
                name: "IX_CianObjectPrices_cian_objectrow_id",
                table: "CianObjectPrices",
                column: "cian_objectrow_id");

            migrationBuilder.AddForeignKey(
                name: "FK_CianObjectPrices_CianObjects_cian_objectrow_id",
                table: "CianObjectPrices",
                column: "cian_objectrow_id",
                principalTable: "CianObjects",
                principalColumn: "row_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
