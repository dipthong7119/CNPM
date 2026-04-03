using Microsoft.AspNetCore.Mvc;
using WMSPro.Models;
using WMSPro.Services;

namespace WMSPro.Controllers
{
    public class VatTuController : Controller
    {
        public IActionResult Index(string? q, string? loai)
        {
            ViewBag.Q = q ?? "";
            ViewBag.Loai = loai ?? "";
            var ds = WmsData.DanhSachVatTu.AsQueryable();
            if (!string.IsNullOrEmpty(q))
                ds = ds.Where(x => x.MaVatTu.Contains(q, StringComparison.OrdinalIgnoreCase)
                    || x.TenVatTu.Contains(q, StringComparison.OrdinalIgnoreCase)
                    || x.LoaiVatTu.Contains(q, StringComparison.OrdinalIgnoreCase)
                    || x.MaLo.Contains(q, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(loai))
                ds = ds.Where(x => x.LoaiVatTu == loai);
            return View(ds.ToList());
        }

        public IActionResult Tao()
        {
            ViewBag.DanhSachNCC = WmsData.DanhSachNhaCungCap;
            ViewBag.DanhSachKho = WmsData.DanhSachKho;
            ViewBag.DanhSachViTri = WmsData.DanhSachViTri.Where(v => v.TinhTrang != "Đầy").ToList();
            return View(new VatTu { MaVatTu = WmsData.GenVatTuId() });
        }

        [HttpPost]
        public IActionResult Tao(VatTu model)
        {
            var ncc = WmsData.DanhSachNhaCungCap.FirstOrDefault(n => n.MaNhaCungCap == model.MaNhaCungCap);
            model.TenNhaCungCap = ncc?.TenNhaCungCap ?? "";
            WmsData.DanhSachVatTu.Insert(0, model);
            TempData["Success"] = $"Đã thêm vật tư {model.TenVatTu} thành công!";
            return RedirectToAction("Index");
        }

        public IActionResult Xem(string id)
        {
            var vt = WmsData.DanhSachVatTu.FirstOrDefault(x => x.MaVatTu == id);
            if (vt == null) return NotFound();
            return View(vt);
        }

        public IActionResult Sua(string id)
        {
            var vt = WmsData.DanhSachVatTu.FirstOrDefault(x => x.MaVatTu == id);
            if (vt == null) return NotFound();
            ViewBag.DanhSachNCC = WmsData.DanhSachNhaCungCap;
            ViewBag.DanhSachKho = WmsData.DanhSachKho;
            ViewBag.DanhSachViTri = WmsData.DanhSachViTri;
            return View(vt);
        }

        [HttpPost]
        public IActionResult Sua(VatTu model)
        {
            var idx = WmsData.DanhSachVatTu.FindIndex(x => x.MaVatTu == model.MaVatTu);
            if (idx >= 0)
            {
                var ncc = WmsData.DanhSachNhaCungCap.FirstOrDefault(n => n.MaNhaCungCap == model.MaNhaCungCap);
                model.TenNhaCungCap = ncc?.TenNhaCungCap ?? "";
                WmsData.DanhSachVatTu[idx] = model;
                TempData["Success"] = $"Đã cập nhật vật tư {model.TenVatTu}!";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Xoa(string id)
        {
            var vt = WmsData.DanhSachVatTu.FirstOrDefault(x => x.MaVatTu == id);
            if (vt != null)
            {
                WmsData.DanhSachVatTu.Remove(vt);
                TempData["Success"] = $"Đã xóa vật tư {vt.TenVatTu}!";
            }
            return RedirectToAction("Index");
        }
    }
}
