using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Units : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Districts_DistrictID",
                table: "Unit");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Floor_FloorID",
                table: "Unit");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_UnitTypes_UnitTypeId",
                table: "Unit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Unit",
                table: "Unit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Floor",
                table: "Floor");

            migrationBuilder.RenameTable(
                name: "Unit",
                newName: "Units");

            migrationBuilder.RenameTable(
                name: "Floor",
                newName: "Floors");

            migrationBuilder.RenameIndex(
                name: "IX_Unit_UnitTypeId",
                table: "Units",
                newName: "IX_Units_UnitTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Unit_FloorID",
                table: "Units",
                newName: "IX_Units_FloorID");

            migrationBuilder.RenameIndex(
                name: "IX_Unit_DistrictID",
                table: "Units",
                newName: "IX_Units_DistrictID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Units",
                table: "Units",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Floors",
                table: "Floors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Districts_DistrictID",
                table: "Units",
                column: "DistrictID",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Floors_FloorID",
                table: "Units",
                column: "FloorID",
                principalTable: "Floors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_UnitTypes_UnitTypeId",
                table: "Units",
                column: "UnitTypeId",
                principalTable: "UnitTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_Districts_DistrictID",
                table: "Units");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Floors_FloorID",
                table: "Units");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_UnitTypes_UnitTypeId",
                table: "Units");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Units",
                table: "Units");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Floors",
                table: "Floors");

            migrationBuilder.RenameTable(
                name: "Units",
                newName: "Unit");

            migrationBuilder.RenameTable(
                name: "Floors",
                newName: "Floor");

            migrationBuilder.RenameIndex(
                name: "IX_Units_UnitTypeId",
                table: "Unit",
                newName: "IX_Unit_UnitTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Units_FloorID",
                table: "Unit",
                newName: "IX_Unit_FloorID");

            migrationBuilder.RenameIndex(
                name: "IX_Units_DistrictID",
                table: "Unit",
                newName: "IX_Unit_DistrictID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Unit",
                table: "Unit",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Floor",
                table: "Floor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_Districts_DistrictID",
                table: "Unit",
                column: "DistrictID",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_Floor_FloorID",
                table: "Unit",
                column: "FloorID",
                principalTable: "Floor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_UnitTypes_UnitTypeId",
                table: "Unit",
                column: "UnitTypeId",
                principalTable: "UnitTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
