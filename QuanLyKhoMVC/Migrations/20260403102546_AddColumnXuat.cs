using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyKhoMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnXuat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SoLuongDaXuat",
                table: "DanhSachVatTu",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoLuongDaXuat",
                table: "DanhSachVatTu");
        }
    }
}
