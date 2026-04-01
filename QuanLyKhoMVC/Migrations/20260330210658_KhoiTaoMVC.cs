using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyKhoMVC.Migrations
{
    /// <inheritdoc />
    public partial class KhoiTaoMVC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DanhSachVatTu",
                columns: table => new
                {
                    MaVatTu = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenVatTu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    ViTri = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhSachVatTu", x => x.MaVatTu);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DanhSachVatTu");
        }
    }
}
