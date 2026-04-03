using Microsoft.AspNetCore.Mvc;
using WMSPro.Models;
using WMSPro.Services;

namespace WMSPro.Controllers
{
    public class XuatKhoController : Controller
    {
        public IActionResult Index(string? filter, string? q)
        {
            ViewBag.Filter = filter ?? "tat-ca";
            ViewBag.Q = q ?? "";
            var ds = WmsData.DanhSachPhieuXuat.AsQueryable();
            if (filter == "hoan-thanh") ds = ds.Where(x => x.TrangThai == "Hoàn thành");
            else if (filter == "dang-soan") ds = ds.Where(x => x.TrangThai == "Đang soạn");
            else if (filter == "cho-duyet") ds = ds.Where(x => x.TrangThai == "Chờ duyệt");
            if (!string.IsNullOrEmpty(q))
                ds = ds.Where(x => x.SoPhieu.Contains(q, StringComparison.OrdinalIgnoreCase)
                    || x.TenHang.Contains(q, StringComparison.OrdinalIgnoreCase)
                    || x.MaHang.Contains(q, StringComparison.OrdinalIgnoreCase));
            return View(ds.ToList());
        }

        public IActionResult Tao()
        {
            ViewBag.DanhSachVatTu = WmsData.DanhSachVatTu;
            return View(new PhieuXuatKho
            {
                SoPhieu = WmsData.GenPhieuXuatId(),
                Ngay = DateTime.Now.ToString("yyyy-MM-dd"),
                TrangThai = "Chờ duyệt"
            });
        }

        [HttpPost]
        public IActionResult Tao(PhieuXuatKho model)
        {
            // B2: Kiểm tra trường bắt buộc
            if (string.IsNullOrEmpty(model.MaHang) || model.SlXuat <= 0)
            {
                TempData["Error"] = "Vui lòng điền đầy đủ thông tin và số lượng xuất > 0!";
                ViewBag.DanhSachVatTu = WmsData.DanhSachVatTu;
                return View(model);
            }
            // B3: Kiểm tra vật tư tồn tại
            var vatTu = WmsData.DanhSachVatTu.FirstOrDefault(v => v.MaVatTu == model.MaHang);
            if (vatTu == null)
            {
                TempData["Error"] = "Mã hàng không tồn tại trong hệ thống!";
                ViewBag.DanhSachVatTu = WmsData.DanhSachVatTu;
                return View(model);
            }
            // B5-B6: Kiểm tra tồn kho trước khi xuất
            if (vatTu.SoLuongTon < model.SlXuat)
            {
                TempData["Error"] = $"Không đủ tồn kho! Hiện có: {vatTu.SoLuongTon} {vatTu.DonViTinh}, cần xuất: {model.SlXuat}";
                ViewBag.DanhSachVatTu = WmsData.DanhSachVatTu;
                return View(model);
            }
            model.TenHang = vatTu.TenVatTu;
            model.MaLo = vatTu.MaLo;
            // B7: Xác định vị trí lấy hàng
            var viTri = WmsData.DanhSachViTri
                .FirstOrDefault(v => v.MatHang.Contains(vatTu.TenVatTu, StringComparison.OrdinalIgnoreCase));
            // B8-B9: Lưu phiếu xuất — tính TonSauXuat TRƯỚC khi trừ
            int tonSauXuat = vatTu.SoLuongTon - model.SlXuat;
            model.TonSauXuat = tonSauXuat;
            WmsData.DanhSachPhieuXuat.Insert(0, model);
            // B10: Cập nhật số lượng tồn khi Hoàn thành
            if (model.TrangThai == "Hoàn thành")
            {
                vatTu.SoLuongTon = tonSauXuat;
            }
            string viTriStr = viTri?.MaViTri ?? vatTu.MaViTri;
            TempData["Success"] = $"Phiếu xuất {model.SoPhieu} tạo thành công! Vị trí lấy hàng: {viTriStr}";
            return RedirectToAction("Index");
        }

        public IActionResult Xem(string id)
        {
            var phieu = WmsData.DanhSachPhieuXuat.FirstOrDefault(x => x.SoPhieu == id);
            if (phieu == null) return NotFound();
            return View(phieu);
        }

        public IActionResult Sua(string id)
        {
            var phieu = WmsData.DanhSachPhieuXuat.FirstOrDefault(x => x.SoPhieu == id);
            if (phieu == null) return NotFound();
            ViewBag.DanhSachVatTu = WmsData.DanhSachVatTu;
            return View(phieu);
        }

        [HttpPost]
        public IActionResult Sua(PhieuXuatKho model)
        {
            var idx = WmsData.DanhSachPhieuXuat.FindIndex(x => x.SoPhieu == model.SoPhieu);
            if (idx >= 0)
            {
                // Recalculate TonSauXuat correctly
                var vatTu = WmsData.DanhSachVatTu.FirstOrDefault(v => v.MaVatTu == model.MaHang);
                if (vatTu != null) model.TonSauXuat = vatTu.SoLuongTon - model.SlXuat;
                WmsData.DanhSachPhieuXuat[idx] = model;
                TempData["Success"] = $"Đã cập nhật {model.SoPhieu}!";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Xoa(string id)
        {
            var phieu = WmsData.DanhSachPhieuXuat.FirstOrDefault(x => x.SoPhieu == id);
            if (phieu != null) { WmsData.DanhSachPhieuXuat.Remove(phieu); TempData["Success"] = $"Đã xóa phiếu {id}!"; }
            return RedirectToAction("Index");
        }
    }
}
