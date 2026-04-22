using System.ComponentModel.DataAnnotations;
using WMSPro.Models;

namespace WMSPro.ViewModels
{
    // ─── Đăng nhập ───
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        public string TenDangNhap { get; set; } = "";

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; } = "";

        public string? ErrorMessage { get; set; }
    }

    // ─── Đăng ký (Admin tạo tài khoản mới) ───
    public class TaoTaiKhoanViewModel
    {
        [Required, MaxLength(50)]
        public string TenDangNhap { get; set; } = "";

        [Required, MaxLength(200)]
        public string HoTen { get; set; } = "";

        [EmailAddress]
        public string? Email { get; set; }

        [Required, MinLength(6)]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; } = "";

        [Compare("MatKhau", ErrorMessage = "Mật khẩu xác nhận không khớp")]
        [DataType(DataType.Password)]
        public string XacNhanMatKhau { get; set; } = "";

        [Required]
        public string VaiTro { get; set; } = "NhanVien";
    }

    // ─── Đổi mật khẩu ───
    public class DoiMatKhauViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string MatKhauCu { get; set; } = "";

        [Required, MinLength(6)]
        [DataType(DataType.Password)]
        public string MatKhauMoi { get; set; } = "";

        [Compare("MatKhauMoi", ErrorMessage = "Mật khẩu mới không khớp")]
        [DataType(DataType.Password)]
        public string XacNhanMatKhauMoi { get; set; } = "";
    }

    // ─── Dashboard ───
    public class DashboardViewModel
    {
        public int TongTonKho { get; set; }
        public int NhapKhoHomNay { get; set; }
        public int XuatKhoHomNay { get; set; }
        public int SoCanhBao { get; set; }
        public List<HoatDongItem> HoatDongGanDay { get; set; } = new();
        public List<VatTu> DanhSachTonKho { get; set; } = new();
        public List<CanhBaoItem> DanhSachCanhBao { get; set; } = new();
    }

    // ─── Items phụ cho dashboard ───
    public class HoatDongItem
    {
        public int Id { get; set; }
        public string SoPhieu { get; set; } = "";
        public string LoaiPhieu { get; set; } = "";
        public string TenHang { get; set; } = "";
        public int SoLuong { get; set; }
        public string TrangThai { get; set; } = "";
        public string NguoiTao { get; set; } = "";
    }

    public class CanhBaoItem
    {
        public string TenHang { get; set; } = "";
        public string MoTa { get; set; } = "";
        public string ThoiGian { get; set; } = "";
        public string Loai { get; set; } = "";
    }

    public class BaoCaoItem
    {
        public string MaHang { get; set; } = "";
        public string TenHang { get; set; } = "";
        public int TonDauKy { get; set; }
        public int TongNhap { get; set; }
        public int TongXuat { get; set; }
        public int TonCuoiKy { get; set; }
        public string DonVi { get; set; } = "cái";
    }


    // ─── Danh sách người dùng (Admin quản lý) ───
    public class NguoiDungViewModel
    {
        public string Id { get; set; } = "";
        public string TenDangNhap { get; set; } = "";
        public string HoTen { get; set; } = "";
        public string? Email { get; set; }
        public string VaiTro { get; set; } = "";
        public string TrangThai { get; set; } = "";
        public DateTime NgayTao { get; set; }
    }

    // ─── Chỉnh sửa tài khoản người dùng (Admin) ───
    public class SuaTaiKhoanViewModel
    {
        public string Id { get; set; } = "";

        [Required, MaxLength(50)]
        public string TenDangNhap { get; set; } = "";

        [Required, MaxLength(200)]
        public string HoTen { get; set; } = "";

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string VaiTroHienTai { get; set; } = "";

        [Required]
        public string VaiTroMoi { get; set; } = "";

        public string TrangThai { get; set; } = "";
    }
}
