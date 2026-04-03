using WMSPro.Models;

namespace WMSPro.Services
{
    public static class WmsData
    {
        // ── NGƯỜI DÙNG ──
        public static List<NguoiDung> DanhSachNguoiDung = new()
        {
            new() { MaNguoiDung="ND-001", TenDangNhap="admin", MatKhau="admin123", HoTen="Quản Lý Kho", VaiTro="Admin", TrangThai="Hoạt động" },
            new() { MaNguoiDung="ND-002", TenDangNhap="thukho", MatKhau="thukho123", HoTen="Nguyễn Văn A", VaiTro="ThuKho", TrangThai="Hoạt động" },
            new() { MaNguoiDung="ND-003", TenDangNhap="nhanvien", MatKhau="nv123", HoTen="Trần Thị B", VaiTro="NhanVien", TrangThai="Hoạt động" },
        };

        // ── NHÀ CUNG CẤP ──
        public static List<NhaCungCap> DanhSachNhaCungCap = new()
        {
            new() { MaNhaCungCap="NCC-001", TenNhaCungCap="Công ty ABC", DiaChi="123 Đường Lê Lợi, Hà Nội", SoDienThoai="024-1234-5678", Email="abc@company.vn" },
            new() { MaNhaCungCap="NCC-002", TenNhaCungCap="Công ty XYZ", DiaChi="456 Nguyễn Huệ, TP.HCM", SoDienThoai="028-8765-4321", Email="xyz@company.vn" },
            new() { MaNhaCungCap="NCC-003", TenNhaCungCap="Tech Supply Co.", DiaChi="789 Trần Phú, Đà Nẵng", SoDienThoai="0236-999-888", Email="techsupply@vn.com" },
        };

        // ── KHO ──
        public static List<Kho> DanhSachKho = new()
        {
            new() { MaKho="KHO-A", TenKho="Kho A - Điện tử", DiaDiem="Tầng 1, Tòa nhà A", NguoiPhuTrach="Nguyễn Văn A", GhiChu="Kho chứa thiết bị điện tử" },
            new() { MaKho="KHO-B", TenKho="Kho B - Phụ kiện", DiaDiem="Tầng 2, Tòa nhà A", NguoiPhuTrach="Trần Thị B", GhiChu="Kho chứa phụ kiện" },
        };

        // ── VẬT TƯ ──
        public static List<VatTu> DanhSachVatTu = new()
        {
            new() { MaVatTu="DT-A12", TenVatTu="Điện thoại A12", DonViTinh="cái", LoaiVatTu="Điện tử", SoLuongTon=245, MucTonToiThieu=50, MaNhaCungCap="NCC-001", TenNhaCungCap="Công ty ABC", MaKho="KHO-A", MaViTri="A1-02", MaLo="LOT-045" },
            new() { MaVatTu="LT-I5", TenVatTu="Laptop Core i5", DonViTinh="cái", LoaiVatTu="Điện tử", SoLuongTon=78, MucTonToiThieu=20, MaNhaCungCap="NCC-003", TenNhaCungCap="Tech Supply Co.", MaKho="KHO-A", MaViTri="B2-01", MaLo="SN-LT-*" },
            new() { MaVatTu="CN-A200", TenVatTu="Chuột không dây A200", DonViTinh="cái", LoaiVatTu="Phụ kiện", SoLuongTon=8, MucTonToiThieu=20, MaNhaCungCap="NCC-001", TenNhaCungCap="Công ty ABC", MaKho="KHO-B", MaViTri="A1-02", MaLo="LOT-M-088" },
            new() { MaVatTu="TN-BT", TenVatTu="Tai nghe Bluetooth", DonViTinh="cái", LoaiVatTu="Phụ kiện", SoLuongTon=32, MucTonToiThieu=15, MaNhaCungCap="NCC-002", TenNhaCungCap="Công ty XYZ", MaKho="KHO-A", MaViTri="C3-04", MaLo="LOT-044" },
            new() { MaVatTu="BF-K300", TenVatTu="Bàn phím cơ K300", DonViTinh="cái", LoaiVatTu="Phụ kiện", SoLuongTon=15, MucTonToiThieu=25, MaNhaCungCap="NCC-003", TenNhaCungCap="Tech Supply Co.", MaKho="KHO-B", MaViTri="D1-02", MaLo="SN-K300-001" },
            new() { MaVatTu="MH-27", TenVatTu="Màn hình 27 inch", DonViTinh="cái", LoaiVatTu="Điện tử", SoLuongTon=42, MucTonToiThieu=10, MaNhaCungCap="NCC-002", TenNhaCungCap="Công ty XYZ", MaKho="KHO-A", MaViTri="D1-03", MaLo="LOT-038" },
        };

        // ── VỊ TRÍ KHO ──
        public static List<ViTri> DanhSachViTri = new()
        {
            new() { MaViTri="A1-01", Loai="Kệ đứng", SucChua="100 kg", TinhTrang="Đầy", MatHang="Điện thoại A12", MaKho="KHO-A" },
            new() { MaViTri="A1-02", Loai="Kệ đứng", SucChua="100 kg", TinhTrang="Một phần", MatHang="Chuột không dây", MaKho="KHO-A" },
            new() { MaViTri="A1-03", Loai="Kệ đứng", SucChua="100 kg", TinhTrang="Trống", MatHang="—", MaKho="KHO-A" },
            new() { MaViTri="B2-01", Loai="Kệ nặng", SucChua="500 kg", TinhTrang="Đầy", MatHang="Laptop Core i5", MaKho="KHO-A" },
            new() { MaViTri="B2-02", Loai="Kệ nặng", SucChua="500 kg", TinhTrang="Một phần", MatHang="Màn hình 27\"", MaKho="KHO-A" },
            new() { MaViTri="C3-04", Loai="Kệ nhỏ", SucChua="50 kg", TinhTrang="Đầy", MatHang="Tai nghe BT", MaKho="KHO-A" },
            new() { MaViTri="D1-02", Loai="Kệ nhỏ", SucChua="50 kg", TinhTrang="Một phần", MatHang="Bàn phím K300", MaKho="KHO-B" },
            new() { MaViTri="D1-03", Loai="Kệ nhỏ", SucChua="50 kg", TinhTrang="Một phần", MatHang="Màn hình 27\"", MaKho="KHO-B" },
        };

        // ── PHIẾU NHẬP ──
        public static List<PhieuNhapKho> DanhSachPhieuNhap = new()
        {
            new() { SoPhieu="PN-2024-889", NgayChungTu="02/04/2024", MaHang="DT-A12", TenHang="Điện thoại A12", MaLo="LOT-2024-045", SlThucTe=50, DonGia=3500000, NhaCungCap="Công ty ABC", MaNhaCungCap="NCC-001", TrangThai="Hoàn thành", MaViTriGoiY="A1-01" },
            new() { SoPhieu="PN-2024-888", NgayChungTu="01/04/2024", MaHang="TN-BT", TenHang="Tai nghe Bluetooth", MaLo="LOT-2024-044", SlThucTe=100, DonGia=850000, NhaCungCap="Công ty XYZ", MaNhaCungCap="NCC-002", TrangThai="Đang xử lý", MaViTriGoiY="C3-04" },
            new() { SoPhieu="PN-2024-887", NgayChungTu="30/03/2024", MaHang="BF-K300", TenHang="Bàn phím cơ K300", MaLo="SN-K300-001", SlThucTe=60, DonGia=1200000, NhaCungCap="Tech Supply Co.", MaNhaCungCap="NCC-003", TrangThai="Chờ duyệt", MaViTriGoiY="D1-02" },
        };

        // ── PHIẾU XUẤT ──
        public static List<PhieuXuatKho> DanhSachPhieuXuat = new()
        {
            new() { SoPhieu="PX-2024-845", Ngay="02/04/2024", MaHang="LT-I5", TenHang="Laptop Core i5", MaLo="SN-LT-812", SlXuat=12, TonSauXuat=78, DoiTac="Siêu thị A", BoPhanNhan="Bộ phận Kinh doanh", LyDoXuat="Bán hàng", TrangThai="Hoàn thành" },
            new() { SoPhieu="PX-2024-844", Ngay="01/04/2024", MaHang="CN-A200", TenHang="Chuột không dây A200", MaLo="LOT-M-888", SlXuat=30, TonSauXuat=8, DoiTac="Đại lý B", BoPhanNhan="Bộ phận Kinh doanh", LyDoXuat="Bán hàng", TrangThai="Hoàn thành" },
            new() { SoPhieu="PX-2024-843", Ngay="30/03/2024", MaHang="DT-A12", TenHang="Điện thoại A12", MaLo="LOT-2024-042", SlXuat=25, TonSauXuat=245, DoiTac="Siêu thị C", BoPhanNhan="Bộ phận Kinh doanh", LyDoXuat="Bán hàng", TrangThai="Đang soạn" },
        };

        // ── CẢNH BÁO ──
        public static List<CanhBaoItem> DanhSachCanhBao = new()
        {
            new() { TenHang="Chuột không dây A200 — Sắp hết hàng", MoTa="Tồn kho: 8 / Mức tối thiểu: 20 đơn vị · Vị trí: A1-02 · Lô: LOT-M-088", ThoiGian="2 giờ trước", Loai="sap-het" },
            new() { TenHang="Bàn phím cơ K300 — Tồn thấp", MoTa="Tồn kho: 15 / Mức tối thiểu: 25 đơn vị · Vị trí: D1-02", ThoiGian="1 ngày trước", Loai="sap-het" },
        };

        // ── BÁO CÁO ──
        public static List<BaoCaoItem> DanhSachBaoCao = new()
        {
            new() { MaHang="DT-A12", TenHang="Điện thoại A12", TonDauKy=228, TongNhap=75, TongXuat=58, TonCuoiKy=245 },
            new() { MaHang="LT-I5", TenHang="Laptop Core i5", TonDauKy=82, TongNhap=20, TongXuat=24, TonCuoiKy=78 },
            new() { MaHang="CN-A200", TenHang="Chuột không dây A200", TonDauKy=48, TongNhap=0, TongXuat=40, TonCuoiKy=8 },
            new() { MaHang="TN-BT", TenHang="Tai nghe Bluetooth", TonDauKy=32, TongNhap=100, TongXuat=100, TonCuoiKy=32 },
            new() { MaHang="BF-K300", TenHang="Bàn phím cơ K300", TonDauKy=0, TongNhap=60, TongXuat=45, TonCuoiKy=15 },
        };

        // ── AUTO-ID HELPERS ──
        public static string GenPhieuNhapId() => $"PN-2024-{890 + DanhSachPhieuNhap.Count(x => x.SoPhieu.StartsWith("PN-2024-8"))}";
        public static string GenPhieuXuatId() => $"PX-2024-{846 + DanhSachPhieuXuat.Count(x => x.SoPhieu.StartsWith("PX-2024-8"))}";
        public static string GenVatTuId() => $"VT-{(DanhSachVatTu.Count + 1):D3}";
        public static string GenNccId() => $"NCC-{(DanhSachNhaCungCap.Count + 1):D3}";
        public static string GenKhoId() => $"KHO-{(char)('A' + DanhSachKho.Count)}";
    }
}
