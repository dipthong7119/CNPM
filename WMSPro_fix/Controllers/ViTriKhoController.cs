using Microsoft.AspNetCore.Mvc;
using WMSPro.Models;
using WMSPro.Services;

namespace WMSPro.Controllers
{
    public class ViTriKhoController : Controller
    {
        public IActionResult Index(string? maKho)
        {
            ViewBag.MaKho = maKho ?? "KHO-A";
            ViewBag.DanhSachKho = WmsData.DanhSachKho;
            var ds = WmsData.DanhSachViTri.AsQueryable();
            if (!string.IsNullOrEmpty(maKho)) ds = ds.Where(v => v.MaKho == maKho);
            return View(ds.ToList());
        }

        public IActionResult Tao()
        {
            ViewBag.DanhSachKho = WmsData.DanhSachKho;
            return View(new ViTri { MaViTri = $"A1-0{WmsData.DanhSachViTri.Count + 1}" });
        }

        [HttpPost]
        public IActionResult Tao(ViTri model)
        {
            if (string.IsNullOrEmpty(model.MaViTri))
            {
                TempData["Error"] = "Mã vị trí không được để trống!";
                ViewBag.DanhSachKho = WmsData.DanhSachKho;
                return View(model);
            }
            if (WmsData.DanhSachViTri.Any(v => v.MaViTri == model.MaViTri))
            {
                TempData["Error"] = $"Mã vị trí {model.MaViTri} đã tồn tại!";
                ViewBag.DanhSachKho = WmsData.DanhSachKho;
                return View(model);
            }
            if (string.IsNullOrEmpty(model.TinhTrang)) model.TinhTrang = "Trống";
            if (string.IsNullOrEmpty(model.MatHang)) model.MatHang = "—";
            WmsData.DanhSachViTri.Add(model);
            TempData["Success"] = $"Đã thêm vị trí {model.MaViTri} thành công!";
            return RedirectToAction("Index", new { maKho = model.MaKho });
        }

        public IActionResult Sua(string id)
        {
            var vt = WmsData.DanhSachViTri.FirstOrDefault(v => v.MaViTri == id);
            if (vt == null) return NotFound();
            ViewBag.DanhSachKho = WmsData.DanhSachKho;
            return View(vt);
        }

        [HttpPost]
        public IActionResult Sua(ViTri model)
        {
            var idx = WmsData.DanhSachViTri.FindIndex(v => v.MaViTri == model.MaViTri);
            if (idx >= 0) { WmsData.DanhSachViTri[idx] = model; TempData["Success"] = $"Đã cập nhật vị trí {model.MaViTri}!"; }
            return RedirectToAction("Index", new { maKho = model.MaKho });
        }

        [HttpPost]
        public IActionResult Xoa(string id)
        {
            var vt = WmsData.DanhSachViTri.FirstOrDefault(v => v.MaViTri == id);
            if (vt != null) { WmsData.DanhSachViTri.Remove(vt); TempData["Success"] = $"Đã xóa vị trí {id}!"; }
            return RedirectToAction("Index");
        }
    }
}
