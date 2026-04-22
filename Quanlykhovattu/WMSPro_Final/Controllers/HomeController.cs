using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WMSPro.Data;
using WMSPro.Models;
using WMSPro.ViewModels;

namespace WMSPro.Controllers
{
    [Authorize] // Mọi action đều yêu cầu đăng nhập
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userMgr;

        public HomeController(AppDbContext db, UserManager<ApplicationUser> userMgr)
        {
            _db = db;
            _userMgr = userMgr;
        }

        public async Task<IActionResult> Index()
        {
            var phieuNhap = await _db.PhieuNhapKhos.OrderByDescending(p => p.Id).Take(10).ToListAsync();
            var phieuXuat = await _db.PhieuXuatKhos.OrderByDescending(p => p.Id).Take(10).ToListAsync();
            var vatTus = await _db.VatTus.ToListAsync();

            var hoatDong = phieuNhap
                .Select(p => new HoatDongItem { Id = p.Id, SoPhieu = p.SoPhieu, LoaiPhieu = "Nhập", TenHang = p.TenHang, SoLuong = p.SlThucTe, TrangThai = p.TrangThai, NguoiTao = p.NguoiTaoHoTen ?? "" })
                .Concat(phieuXuat.Select(p => new HoatDongItem { Id = p.Id, SoPhieu = p.SoPhieu, LoaiPhieu = "Xuất", TenHang = p.TenHang, SoLuong = -p.SlXuat, TrangThai = p.TrangThai, NguoiTao = p.NguoiTaoHoTen ?? "" }))
                .OrderByDescending(x => x.SoPhieu).Take(10).ToList();

            var canhBao = vatTus
                .Where(v => v.SoLuongTon <= v.MucTonToiThieu)
                .Select(v => new CanhBaoItem
                {
                    TenHang = $"{v.TenVatTu} — Sắp hết hàng",
                    MoTa = $"Tồn kho: {v.SoLuongTon} / Mức tối thiểu: {v.MucTonToiThieu} · Vị trí: {v.MaViTri}",
                    ThoiGian = "Vừa cập nhật",
                    Loai = "sap-het"
                }).ToList();

            // Nhập / Xuất hôm nay
            var homNay = DateTime.Today;
            int nhapHomNay = await _db.PhieuNhapKhos.Where(p => p.NgayChungTu.Date == homNay).SumAsync(p => (int?)p.SlThucTe) ?? 0;
            int xuatHomNay = await _db.PhieuXuatKhos.Where(p => p.Ngay.Date == homNay).SumAsync(p => (int?)p.SlXuat) ?? 0;

            var vm = new DashboardViewModel
            {
                TongTonKho = vatTus.Sum(v => v.SoLuongTon),
                NhapKhoHomNay = nhapHomNay,
                XuatKhoHomNay = xuatHomNay,
                SoCanhBao = canhBao.Count,
                HoatDongGanDay = hoatDong,
                DanhSachTonKho = vatTus,
                DanhSachCanhBao = canhBao
            };

            return View(vm);
        }
    }
}
