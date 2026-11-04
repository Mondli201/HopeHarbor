using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HopeHarbor.Migrations
{
    /// <inheritdoc />
    public partial class Part__2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Disaster",
                columns: table => new
                {
                    DisasterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredAidTypes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disaster", x => x.DisasterId);
                });

            migrationBuilder.CreateTable(
                name: "GoodsCategory",
                columns: table => new
                {
                    CategoryName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsCategory", x => x.CategoryName);
                });

            migrationBuilder.CreateTable(
                name: "MonetaryDonation",
                columns: table => new
                {
                    DonationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonorName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DonationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    IsAnonymous = table.Column<bool>(type: "bit", nullable: false),
                    DonationDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonetaryDonation", x => x.DonationId);
                });

            migrationBuilder.CreateTable(
                name: "Money_Allocation",
                columns: table => new
                {
                    MoneyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisasterId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Money_Allocation", x => x.MoneyID);
                    table.ForeignKey(
                        name: "FK_Money_Allocation_Disaster_DisasterId",
                        column: x => x.DisasterId,
                        principalTable: "Disaster",
                        principalColumn: "DisasterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodsDonation",
                columns: table => new
                {
                    DonationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonorName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DonationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ItemCategory = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ItemCount = table.Column<int>(type: "int", nullable: false),
                    IsAnonymous = table.Column<bool>(type: "bit", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsDonation", x => x.DonationId);
                    table.ForeignKey(
                        name: "FK_GoodsDonation_GoodsCategory_CategoryName",
                        column: x => x.CategoryName,
                        principalTable: "GoodsCategory",
                        principalColumn: "CategoryName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Good_Allocation",
                columns: table => new
                {
                    Goods_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisasterId = table.Column<int>(type: "int", nullable: false),
                    DonationId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Good_Allocation", x => x.Goods_Id);
                    table.ForeignKey(
                        name: "FK_Good_Allocation_Disaster_DisasterId",
                        column: x => x.DisasterId,
                        principalTable: "Disaster",
                        principalColumn: "DisasterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Good_Allocation_GoodsDonation_DonationId",
                        column: x => x.DonationId,
                        principalTable: "GoodsDonation",
                        principalColumn: "DonationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Good_Allocation_DisasterId",
                table: "Good_Allocation",
                column: "DisasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Good_Allocation_DonationId",
                table: "Good_Allocation",
                column: "DonationId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsDonation_CategoryName",
                table: "GoodsDonation",
                column: "CategoryName");

            migrationBuilder.CreateIndex(
                name: "IX_Money_Allocation_DisasterId",
                table: "Money_Allocation",
                column: "DisasterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Good_Allocation");

            migrationBuilder.DropTable(
                name: "MonetaryDonation");

            migrationBuilder.DropTable(
                name: "Money_Allocation");

            migrationBuilder.DropTable(
                name: "GoodsDonation");

            migrationBuilder.DropTable(
                name: "Disaster");

            migrationBuilder.DropTable(
                name: "GoodsCategory");
        }
    }
}
