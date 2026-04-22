using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WMSPro.Models
{
    public class VatTu
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string MaVatTu { get; set; } = "";

        [Required, MaxLength(200)]
        public string TenVatTu { get; set; } = "";

        [MaxLength(20)]
        public string DonViTinh { get; set; } = "cái";

        [MaxLength(100)]
        public string LoaiVatTu { get; set; } = "";

        public int SoLuongTon { get; set; }
        public int MucTonToiThieu { get; set; }

        [MaxLength(20)]
        public string MaNhaCungCap { get; set; } = "";

        [MaxLength(200)]
        public string TenNhaCungCap { get; set; } = "";

        [MaxLength(20)]
        public string MaKho { get; set; } = "";

        [MaxLength(20)]
        public string MaViTri { get; set; } = "";

        [MaxLength(50)]
        public string MaLo { get; set; } = "";

        [MaxLength(100)]
        public string SoSeri { get; set; } = "";

        // Không lưu vào DB - tính toán
        [NotMapped]
        public string TrangThai => SoLuongTon <= 0 ? "danger"
    : SoLuongTon <= MucTonToiThieu ? "warning" : "normal";
    }
}
