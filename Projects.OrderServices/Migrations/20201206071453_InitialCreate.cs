using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace Projects.OrderServices.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    OrderType = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    OrderSn = table.Column<string>(nullable: true),
                    OrderTotalPrice = table.Column<string>(nullable: true),
                    Createtime = table.Column<DateTime>(nullable: false),
                    Updatetime = table.Column<DateTime>(nullable: false),
                    Paytime = table.Column<DateTime>(nullable: false),
                    Sendtime = table.Column<DateTime>(nullable: false),
                    Successtime = table.Column<DateTime>(nullable: false),
                    OrderStatus = table.Column<int>(nullable: false),
                    OrderName = table.Column<string>(nullable: true),
                    OrderTel = table.Column<string>(nullable: true),
                    OrderAddress = table.Column<string>(nullable: true),
                    OrderRemark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(nullable: false),
                    OrderSn = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    ProductUrl = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    ItemPrice = table.Column<decimal>(nullable: false),
                    ItemCount = table.Column<int>(nullable: false),
                    ItemTotalPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
