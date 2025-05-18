using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cineplex_Management.Migrations
{
    /// <inheritdoc />
    public partial class initialModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HALL",
                columns: table => new
                {
                    HallId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HallName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Row = table.Column<int>(type: "int", nullable: true),
                    Column = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HALL__7E60E214602EBDF7", x => x.HallId);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProducerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Movie__4BD2941AB3599871", x => x.MovieId);
                });

            migrationBuilder.CreateTable(
                name: "paymentType",
                columns: table => new
                {
                    PaymentTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentTypeName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__paymentT__BA430B35F7254E4C", x => x.PaymentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    RoleName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Role__8AFACE1AADCCF4E1", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    SalesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    Time = table.Column<DateTime>(type: "datetime", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalPay = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Sale__C952FB32D7424E5C", x => x.SalesId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Mobile = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__1788CC4CCCE0B714", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "SeatPlan",
                columns: table => new
                {
                    SeatPlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    HallId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SeatPlan__9C7E27CA1F9584FC", x => x.SeatPlanId);
                    table.ForeignKey(
                        name: "FK__SeatPlan__HallId__5070F446",
                        column: x => x.HallId,
                        principalTable: "HALL",
                        principalColumn: "HallId");
                });

            migrationBuilder.CreateTable(
                name: "Show",
                columns: table => new
                {
                    ShowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    MovieId = table.Column<int>(type: "int", nullable: true),
                    HallId = table.Column<int>(type: "int", nullable: true),
                    ShowStart = table.Column<DateTime>(type: "datetime", nullable: true),
                    ShowEnd = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Show__6DE3E0B26634086D", x => x.ShowId);
                    table.ForeignKey(
                        name: "FK__Show__HallId__45F365D3",
                        column: x => x.HallId,
                        principalTable: "HALL",
                        principalColumn: "HallId");
                    table.ForeignKey(
                        name: "FK__Show__MovieId__44FF419A",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "MovieId");
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: true),
                    AccNo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    BankName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true),
                    Time = table.Column<TimeOnly>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payment__9B556A38A8B29A4E", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK__Payment__Payment__5629CD9C",
                        column: x => x.PaymentTypeId,
                        principalTable: "paymentType",
                        principalColumn: "PaymentTypeId");
                    table.ForeignKey(
                        name: "FK__Payment__SalesId__5535A963",
                        column: x => x.SalesId,
                        principalTable: "Sale",
                        principalColumn: "SalesId");
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserRole__3D978A3516B8DD19", x => x.UserRoleId);
                    table.ForeignKey(
                        name: "FK__UserRole__RoleId__3C69FB99",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId");
                    table.ForeignKey(
                        name: "FK__UserRole__UserId__3B75D760",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ShowDetails",
                columns: table => new
                {
                    ShowDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowId = table.Column<int>(type: "int", nullable: true),
                    HallId = table.Column<int>(type: "int", nullable: true),
                    SeatNumber = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    IsBooked = table.Column<bool>(type: "bit", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ShowDeta__07FA4CECA16A51B5", x => x.ShowDetailsId);
                    table.ForeignKey(
                        name: "FK__ShowDetai__HallI__49C3F6B7",
                        column: x => x.HallId,
                        principalTable: "HALL",
                        principalColumn: "HallId");
                    table.ForeignKey(
                        name: "FK__ShowDetai__ShowI__48CFD27E",
                        column: x => x.ShowId,
                        principalTable: "Show",
                        principalColumn: "ShowId");
                });

            migrationBuilder.CreateTable(
                name: "SalesDetails",
                columns: table => new
                {
                    SalesDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: true),
                    ShowDetailsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SalesDet__FE1B9AC55CAE1545", x => x.SalesDetailsId);
                    table.ForeignKey(
                        name: "FK__SalesDeta__Sales__4CA06362",
                        column: x => x.SalesId,
                        principalTable: "Sale",
                        principalColumn: "SalesId");
                    table.ForeignKey(
                        name: "FK__SalesDeta__ShowD__4D94879B",
                        column: x => x.ShowDetailsId,
                        principalTable: "ShowDetails",
                        principalColumn: "ShowDetailsId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_PaymentTypeId",
                table: "Payment",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_SalesId",
                table: "Payment",
                column: "SalesId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDetails_SalesId",
                table: "SalesDetails",
                column: "SalesId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDetails_ShowDetailsId",
                table: "SalesDetails",
                column: "ShowDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatPlan_HallId",
                table: "SeatPlan",
                column: "HallId");

            migrationBuilder.CreateIndex(
                name: "IX_Show_HallId",
                table: "Show",
                column: "HallId");

            migrationBuilder.CreateIndex(
                name: "IX_Show_MovieId",
                table: "Show",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowDetails_HallId",
                table: "ShowDetails",
                column: "HallId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowDetails_ShowId",
                table: "ShowDetails",
                column: "ShowId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "SalesDetails");

            migrationBuilder.DropTable(
                name: "SeatPlan");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "paymentType");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "ShowDetails");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Show");

            migrationBuilder.DropTable(
                name: "HALL");

            migrationBuilder.DropTable(
                name: "Movie");
        }
    }
}
