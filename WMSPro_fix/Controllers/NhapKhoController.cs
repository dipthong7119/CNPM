using Microsoft.AspNetCore.Mvc;
using WMSPro.Models;
using WMSPro.Services;

namespace WMSPro.Controllers
{
    public class NhapKhoController : Controller
    {
        public IActionResult Index(string? filter, string? q)
        {
            ViewBag.Filter = filter ?? "tat-ca";
            ViewBag.Q = q ?? "";
            var ds = WmsData.DanhSachPhieuNhap.AsQueryable();
            if (filter == "hoan-thanh") ds = ds.Where(x => x.TrangThai == "Hoàn thành");
            else if (filter == "dang-xu-ly") ds = ds.Where(x => x.TrangThai == "Đang xử lý");
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
            ViewBag.DanhSachNCC = WmsData.DanhSachNhaCungCap;
            ViewBag.DanhSachViTriTrong = WmsData.DanhSachViTri.Where(v => v.TinhTrang != "Đầy").ToList();
            return View(new PhieuNhapKho
            {
                SoPhieu = WmsData.GenPhieuNhapId(),
                NgayChungTu = DateTime.Now.ToString("yyyy-MM-dd"),
                TrangThai = "Chờ duyệt"
            });
        }

        [HttpPost]
        public IActionResult Tao(PhieuNhapKho model)
        {
            // B2: Kiểm tra các trường bắt buộc
            if (string.IsNullOrEmpty(model.MaHang) || model.SlThucTe <= 0)
            {
                TempData["Error"] = "Vui lòng điền đầy đủ thông tin và số lượng > 0!";
                ViewBag.DanhSachVatTu = WmsData.DanhSachVatTu;
                ViewBag.DanhSachNCC = WmsData.DanhSachNhaCungCap;
                ViewBag.DanhSachViTriTrong = WmsData.DanhSachViTri.Where(v => v.TinhTrang != "Đầy").ToList();
                return View(model);
            }
            // B3: Kiểm tra vật tư tồn tại
            var vatTu = WmsData.DanhSachVatTu.FirstOrDefault(v => v.MaVatTu == model.MaHang);
            if (vatTu == null)
            {
                TempData["Error"] = "Mã hàng không tồn tại trong hệ thống!";
                ViewBag.DanhSachVatTu = WmsData.DanhSachVatTu;
                ViewBag.DanhSachNCC = WmsData.DanhSachNhaCungCap;
                ViewBag.DanhSachViTriTrong = WmsData.DanhSachViTri.Where(v => v.TinhTrang != "Đầy").ToList();
                return View(model);
            }
            // B4: Gợi ý vị trí
            var viTriGoiY = WmsData.DanhSachViTri.FirstOrDefault(v => v.TinhTrang == "Trống" || v.TinhTrang == "Một phần");
            model.MaViTriGoiY = viTriGoiY?.MaViTri ?? "Chưa có vị trí trống";
            model.TenHang = vatTu.TenVatTu;
            var ncc = WmsData.DanhSachNhaCungCap.FirstOrDefault(n => n.MaNhaCungCap == model.MaNhaCungCap);
            model.NhaCungCap = ncc?.TenNhaCungCap ?? model.NhaCungCap;
            // B5-B6: Lưu phiếu
            WmsData.DanhSachPhieuNhap.Insert(0, model);
            // B7: Cập nhật số lượng tồn
            if (model.TrangThai == "Hoàn thành")
            {
                vatTu.SoLuongTon += model.SlThucTe; // SoLuongTonMoi = SoLuongTonCu + SoLuongNhap
            }
            TempData["Success"] = $"Phiếu nhập {model.SoPhieu} đã được tạo thành công! Vị trí gợi ý: {model.MaViTriGoiY}";
            return RedirectToAction("Index");
        }

        public IActionResult Xem(string id)
        {
            var phieu = WmsData.DanhSachPhieuNhap.FirstOrDefault(x => x.SoPhieu == id);
            if (phieu == null) return NotFound();
            return View(phieu);
        }

        public IActionResult Sua(string id)
        {
            var phieu = WmsData.DanhSachPhieuNhap.FirstOrDefault(x => x.SoPhieu == id);
            if (phieu == null) return NotFound();
            ViewBag.DanhSachVatTu = WmsData.DanhSachVatTu;
            ViewBag.DanhSachNCC = WmsData.DanhSachNhaCungCap;
            return View(phieu);
        }

        [HttpPost]
        public IActionResult Sua(PhieuNhapKho model)
        {
            var idx = WmsData.DanhSachPhieuNhap.FindIndex(x => x.SoPhieu == model.SoPhieu);
            if (idx >= 0)
            {
                // Cập nhật tên nhà cung cấp từ mã
                var ncc = WmsData.DanhSachNhaCungCap.FirstOrDefault(n => n.MaNhaCungCap == model.MaNhaCungCap);
                if (ncc != null)
                    model.NhaCungCap = ncc.TenNhaCungCap;
                
                WmsData.DanhSachPhieuNhap[idx] = model;
                TempData["Success"] = $"Đã cập nhật {model.SoPhieu}!";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Xoa(string id)
        {
            var phieu = WmsData.DanhSachPhieuNhap.FirstOrDefault(x => x.SoPhieu == id);
            if (phieu != null) { WmsData.DanhSachPhieuNhap.Remove(phieu); TempData["Success"] = $"Đã xóa phiếu {id}!"; }
            return RedirectToAction("Index");
        }
    }
}
