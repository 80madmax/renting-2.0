using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CommunalExpensesandbills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommunalExpenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunalExpenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommunalBills",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    CommunalExpenseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunalBills", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CommunalBills_CommunalExpenses_CommunalExpenseId",
                        column: x => x.CommunalExpenseId,
                        principalTable: "CommunalExpenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommunalBills_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommunalBills_CommunalExpenseId",
                table: "CommunalBills",
                column: "CommunalExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunalBills_UnitId",
                table: "CommunalBills",
                column: "UnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommunalBills");

            migrationBuilder.DropTable(
                name: "CommunalExpenses");
        }
    }
}
