using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoMVC.Models
{
    public class VatTu
    {
        [Key] // Đánh dấu đây là khóa chính
        public string MaVatTu { get; set; } = string.Empty;
        public string TenVatTu { get; set; } = string.Empty;
        public int SoLuong { get; set; }
        public string ViTri { get; set; } = string.Empty;
    }
}