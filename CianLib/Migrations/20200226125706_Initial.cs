using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CianLib.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CianObjects",
                columns: table => new
                {
                    row_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    insert_date = table.Column<DateTime>(type: "date", nullable: false),
                    city = table.Column<int>(nullable: false),
                    cian_id = table.Column<int>(nullable: false),
                    category = table.Column<string>(nullable: true),
                    village_id = table.Column<int>(nullable: true),
                    added = table.Column<int>(nullable: false),
                    house_id = table.Column<int>(nullable: true),
                    newobject_id = table.Column<int>(nullable: true),
                    photo = table.Column<string>(nullable: true),
                    object_type = table.Column<int>(nullable: true),
                    lon = table.Column<float>(nullable: false),
                    filter_type = table.Column<string>(nullable: true),
                    creation_date = table.Column<DateTime>(nullable: false),
                    deal_type = table.Column<string>(nullable: true),
                    from_developer = table.Column<bool>(nullable: false),
                    lat = table.Column<float>(nullable: false),
                    service_id = table.Column<int>(nullable: false),
                    property_type = table.Column<int>(nullable: false),
                    id = table.Column<string>(nullable: true),
                    type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CianObjects", x => x.row_id);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    row_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    insert_date = table.Column<DateTime>(type: "date", nullable: false),
                    city = table.Column<int>(nullable: false),
                    cian_id = table.Column<int>(nullable: false),
                    category = table.Column<string>(nullable: true),
                    village_id = table.Column<int>(nullable: true),
                    added = table.Column<int>(nullable: false),
                    house_id = table.Column<int>(nullable: true),
                    newobject_id = table.Column<int>(nullable: true),
                    photo = table.Column<string>(nullable: true),
                    price = table.Column<long>(nullable: false),
                    object_type = table.Column<int>(nullable: true),
                    lon = table.Column<float>(nullable: false),
                    filter_type = table.Column<string>(nullable: true),
                    creation_date = table.Column<DateTime>(nullable: false),
                    deal_type = table.Column<string>(nullable: true),
                    from_developer = table.Column<bool>(nullable: false),
                    lat = table.Column<float>(nullable: false),
                    service_id = table.Column<int>(nullable: false),
                    property_type = table.Column<int>(nullable: false),
                    id = table.Column<string>(nullable: true),
                    type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.row_id);
                });

            migrationBuilder.CreateTable(
                name: "CianObjectPrices",
                columns: table => new
                {
                    row_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    insert_date = table.Column<DateTime>(type: "date", nullable: false),
                    price = table.Column<long>(nullable: false),
                    cian_objectrow_id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CianObjectPrices", x => x.row_id);
                    table.ForeignKey(
                        name: "FK_CianObjectPrices_CianObjects_cian_objectrow_id",
                        column: x => x.cian_objectrow_id,
                        principalTable: "CianObjects",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CianObjectPrices_cian_objectrow_id",
                table: "CianObjectPrices",
                column: "cian_objectrow_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CianObjectPrices");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "CianObjects");
        }
    }
}
