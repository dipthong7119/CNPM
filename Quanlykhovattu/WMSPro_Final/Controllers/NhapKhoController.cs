using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WMSPro.Data;
using WMSPro.Models;

namespace WMSPro.Controllers
{
    [Authorize] // Xem được khi đã đăng nhập
    public class NhapKhoController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userMgr;

        public NhapKhoController(AppDbContext db, UserManager<ApplicationUser> userMgr)
        {
            _db = db;
            _userMgr = userMgr;
        }

        public async Task<IActionResult> Index(string? filter, string? q)
        {
            ViewBag.Filter = filter ?? "tat-ca";
            ViewBag.Q = q ?? "";

            var ds = _db.PhieuNhapKhos.AsQueryable();
            if (filter == "hoan-thanh") ds = ds.Where(x => x.TrangThai == "Hoàn thành");
            else if (filter == "dang-xu-ly") ds = ds.Where(x => x.TrangThai == "Đang xử lý");
            else if (filter == "cho-duyet") ds = ds.Where(x => x.TrangThai == "Chờ duyệt");

            if (!string.IsNullOrEmpty(q))
                ds = ds.Where(x => x.SoPhieu.Contains(q) || x.TenHang.Contains(q) || x.MaHang.Contains(q));

            return View(await ds.OrderByDescending(x => x.Id).ToListAsync());
        }

        // Tạo phiếu: Tất cả người dùng
        [Authorize(Roles = VaiTroConst.TatCa)]
        public async Task<IActionResult> Tao()
        {
            await LoadViewBag();
            return View(new PhieuNhapKho
            {
                SoPhieu = "",  // Để trống - cho user nhập
                NgayChungTu = DateTime.Now,
                TrangThai = "Chờ duyệt"
            });
        }

        [HttpPost, Authorize(Roles = VaiTroConst.TatCa)]
        public async Task<IActionResult> Tao(PhieuNhapKho model)
        {
            // Validate số phiếu
            if (string.IsNullOrEmpty(model.SoPhieu))
            {
                TempData["Error"] = "Vui lòng nhập số phiếu!";
                await LoadViewBag(); return View(model);
            }

            // Kiểm tra số phiếu đã tồn tại
            if (await _db.PhieuNhapKhos.AnyAsync(x => x.SoPhieu == model.SoPhieu))
            {
                TempData["Error"] = $"Số phiếu {model.SoPhieu} đã tồn tại!";
                await LoadViewBag(); return View(model);
            }

            if (string.IsNullOrEmpty(model.MaHang) || model.SlThucTe <= 0)
            {
                TempData["Error"] = "Vui lòng điền đầy đủ thông tin và số lượng > 0!";
                await LoadViewBag(); return View(model);
            }

            var vatTu = await _db.VatTus.FirstOrDefaultAsync(v => v.MaVatTu == model.MaHang);
            if (vatTu == null)
            {
                TempData["Error"] = "Mã hàng không tồn tại trong hệ thống!";
                await LoadViewBag(); return View(model);
            }

            // Gợi ý vị trí
            var viTriGoiY = await _db.ViTris.FirstOrDefaultAsync(v => v.TinhTrang == "Trống" || v.TinhTrang == "Một phần");
            model.MaViTriGoiY = viTriGoiY?.MaViTri ?? "Chưa có vị trí trống";
            model.TenHang = vatTu.TenVatTu;

            var ncc = await _db.NhaCungCaps.FirstOrDefaultAsync(n => n.MaNhaCungCap == model.MaNhaCungCap);
            model.NhaCungCap = ncc?.TenNhaCungCap ?? model.NhaCungCap;

            // Đảm bảo các field string không bị null (SQLite NOT NULL constraint)
            model.GhiChu ??= "";
            model.TenHang ??= "";
            model.MaLo ??= "";
            model.MaViTriGoiY ??= "";
            model.NhaCungCap ??= "";
            model.MaNhaCungCap ??= "";

            // Lưu người tạo
            var user = await _userMgr.GetUserAsync(User);
            model.NguoiTaoId = user?.Id ?? "";
            model.NguoiTaoHoTen = user?.HoTen ?? "";

            _db.PhieuNhapKhos.Add(model);

            // Cập nhật tồn kho nếu hoàn thành
            if (model.TrangThai == "Hoàn thành")
                vatTu.SoLuongTon += model.SlThucTe;

            await _db.SaveChangesAsync();
            TempData["Success"] = $"Phiếu nhập {model.SoPhieu} tạo thành công! Vị trí gợi ý: {model.MaViTriGoiY}";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Xem(int id)
        {
            var phieu = await _db.PhieuNhapKhos.FindAsync(id);
            if (phieu == null) return NotFound();
            return View(phieu);
        }

        [Authorize(Roles = VaiTroConst.QuanLyVaAdmin)]  // Quản lý + Admin phê duyệt/sửa
        public async Task<IActionResult> Sua(int id)
        {
            var phieu = await _db.PhieuNhapKhos.FindAsync(id);
            if (phieu == null) return NotFound();
            ViewBag.DanhSachVatTu = await _db.VatTus.ToListAsync();
            ViewBag.DanhSachNCC = await _db.NhaCungCaps.ToListAsync();
            return View(phieu);
        }

        [HttpPost, Authorize(Roles = VaiTroConst.QuanLyVaAdmin)]  // Quản lý + Admin phê duyệt
        public async Task<IActionResult> Sua(PhieuNhapKho model)
        {
            // Đảm bảo các field string không bị null (SQLite NOT NULL constraint)
            model.GhiChu ??= "";
            model.TenHang ??= "";
            model.MaLo ??= "";
            model.MaViTriGoiY ??= "";
            model.NguoiTaoId ??= "";
            model.NguoiTaoHoTen ??= "";

            // Lấy tên NCC từ mã
            var ncc = await _db.NhaCungCaps.FirstOrDefaultAsync(n => n.MaNhaCungCap == model.MaNhaCungCap);
            model.NhaCungCap = ncc?.TenNhaCungCap ?? model.NhaCungCap ?? "";

            _db.PhieuNhapKhos.Update(model);
            await _db.SaveChangesAsync();
            TempData["Success"] = $"Đã cập nhật {model.SoPhieu}!";
            return RedirectToAction("Index");
        }

        [HttpPost, Authorize(Roles = VaiTroConst.Admin)]  // Chỉ Admin xóa
        public async Task<IActionResult> Xoa(int id)
        {
            var phieu = await _db.PhieuNhapKhos.FindAsync(id);
            if (phieu != null)
            {
                _db.PhieuNhapKhos.Remove(phieu);
                await _db.SaveChangesAsync();
                TempData["Success"] = $"Đã xóa phiếu {phieu.SoPhieu}!";
            }
            return RedirectToAction("Index");
        }

        private async Task LoadViewBag()
        {
            ViewBag.DanhSachVatTu = await _db.VatTus.ToListAsync();
            ViewBag.DanhSachNCC = await _db.NhaCungCaps.ToListAsync();
            ViewBag.DanhSachViTriTrong = await _db.ViTris.Where(v => v.TinhTrang != "Đầy").ToListAsync();
        }
    }
}
