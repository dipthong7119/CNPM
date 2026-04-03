namespace WMSPro.Models
{
    public class NguoiDung
    {
        public string MaNguoiDung { get; set; } = "";
        public string TenDangNhap { get; set; } = "";
        public string MatKhau { get; set; } = "";
        public string HoTen { get; set; } = "";
        public string VaiTro { get; set; } = "";
        public string TrangThai { get; set; } = "Hoạt động";
    }

    public class VatTu
    {
        public string MaVatTu { get; set; } = "";
        public string TenVatTu { get; set; } = "";
        public string DonViTinh { get; set; } = "cái";
        public string LoaiVatTu { get; set; } = "";
        public int SoLuongTon { get; set; }
        public int MucTonToiThieu { get; set; }
        public string MaNhaCungCap { get; set; } = "";
        public string TenNhaCungCap { get; set; } = "";
        public string MaKho { get; set; } = "";
        public string MaViTri { get; set; } = "";
        public string MaLo { get; set; } = "";
        public string SoSeri { get; set; } = "";
        public string TrangThai => SoLuongTon <= 0 ? "danger" : SoLuongTon <= MucTonToiThieu ? "warning" : "normal";
    }

    public class Kho
    {
        public string MaKho { get; set; } = "";
        public string TenKho { get; set; } = "";
        public string DiaDiem { get; set; } = "";
        public string NguoiPhuTrach { get; set; } = "";
        public string GhiChu { get; set; } = "";
    }

    public class ViTri
    {
        public string MaViTri { get; set; } = "";
        public string Loai { get; set; } = "";
        public string SucChua { get; set; } = "";
        public string TinhTrang { get; set; } = "";
        public string MatHang { get; set; } = "";
        public string MaKho { get; set; } = "KHO-A";
    }

    public class NhaCungCap
    {
        public string MaNhaCungCap { get; set; } = "";
        public string TenNhaCungCap { get; set; } = "";
        public string DiaChi { get; set; } = "";
        public string SoDienThoai { get; set; } = "";
        public string Email { get; set; } = "";
    }

    public class PhieuNhapKho
    {
        public string SoPhieu { get; set; } = "";
        public string NgayChungTu { get; set; } = "";
        public string MaHang { get; set; } = "";
        public string TenHang { get; set; } = "";
        public string MaLo { get; set; } = "";
        public int SlThucTe { get; set; }
        public long DonGia { get; set; }
        public long ThanhTien => SlThucTe * DonGia;
        public string NhaCungCap { get; set; } = "";
        public string MaNhaCungCap { get; set; } = "";
        public string TrangThai { get; set; } = "Chờ duyệt";
        public string GhiChu { get; set; } = "";
        public string MaViTriGoiY { get; set; } = "";
    }

    public class PhieuXuatKho
    {
        public string SoPhieu { get; set; } = "";
        public string Ngay { get; set; } = "";
        public string MaHang { get; set; } = "";
        public string TenHang { get; set; } = "";
        public string MaLo { get; set; } = "";
        public int SlXuat { get; set; }
        public int TonSauXuat { get; set; }
        public string DoiTac { get; set; } = "";
        public string BoPhanNhan { get; set; } = "";
        public string LyDoXuat { get; set; } = "";
        public string TrangThai { get; set; } = "Chờ duyệt";
        public string GhiChu { get; set; } = "";
    }

    public class CanhBaoItem
    {
        public string TenHang { get; set; } = "";
        public string MoTa { get; set; } = "";
        public string ThoiGian { get; set; } = "";
        public string Loai { get; set; } = "";
    }

    public class HoatDongItem
    {
        public string SoPhieu { get; set; } = "";
        public string LoaiPhieu { get; set; } = ""; // "Nhập" hoặc "Xuất"
        public string TenHang { get; set; } = "";
        public int SoLuong { get; set; }
        public string TrangThai { get; set; } = "";
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

    public class LoginViewModel
    {
        public string TenDangNhap { get; set; } = "";
        public string MatKhau { get; set; } = "";
        public string? ErrorMessage { get; set; }
    }
}
