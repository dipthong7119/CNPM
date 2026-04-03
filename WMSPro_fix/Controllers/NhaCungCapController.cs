using Microsoft.AspNetCore.Mvc;
using WMSPro.Models;
using WMSPro.Services;

namespace WMSPro.Controllers
{
    public class NhaCungCapController : Controller
    {
        public IActionResult Index(string? q)
        {
            ViewBag.Q = q ?? "";
            var ds = WmsData.DanhSachNhaCungCap.AsQueryable();
            if (!string.IsNullOrEmpty(q))
                ds = ds.Where(x => x.TenNhaCungCap.Contains(q, StringComparison.OrdinalIgnoreCase)
                    || x.MaNhaCungCap.Contains(q, StringComparison.OrdinalIgnoreCase)
                    || x.SoDienThoai.Contains(q, StringComparison.OrdinalIgnoreCase));
            return View(ds.ToList());
        }

        public IActionResult Tao() => View(new NhaCungCap { MaNhaCungCap = WmsData.GenNccId() });

        [HttpPost]
        public IActionResult Tao(NhaCungCap model)
        {
            WmsData.DanhSachNhaCungCap.Insert(0, model);
            TempData["Success"] = $"Đã thêm nhà cung cấp {model.TenNhaCungCap}!";
            return RedirectToAction("Index");
        }

        public IActionResult Xem(string id)
        {
            var ncc = WmsData.DanhSachNhaCungCap.FirstOrDefault(x => x.MaNhaCungCap == id);
            if (ncc == null) return NotFound();
            ViewBag.VatTuCuaNCC = WmsData.DanhSachVatTu.Where(v => v.MaNhaCungCap == id).ToList();
            return View(ncc);
        }

        public IActionResult Sua(string id)
        {
            var ncc = WmsData.DanhSachNhaCungCap.FirstOrDefault(x => x.MaNhaCungCap == id);
            if (ncc == null) return NotFound();
            return View(ncc);
        }

        [HttpPost]
        public IActionResult Sua(NhaCungCap model)
        {
            var idx = WmsData.DanhSachNhaCungCap.FindIndex(x => x.MaNhaCungCap == model.MaNhaCungCap);
            if (idx >= 0) { WmsData.DanhSachNhaCungCap[idx] = model; TempData["Success"] = $"Đã cập nhật {model.TenNhaCungCap}!"; }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Xoa(string id)
        {
            var ncc = WmsData.DanhSachNhaCungCap.FirstOrDefault(x => x.MaNhaCungCap == id);
            if (ncc != null) { WmsData.DanhSachNhaCungCap.Remove(ncc); TempData["Success"] = $"Đã xóa {ncc.TenNhaCungCap}!"; }
            return RedirectToAction("Index");
        }
    }
}
