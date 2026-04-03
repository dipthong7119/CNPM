using Microsoft.AspNetCore.Mvc;
using WMSPro.Models;
using WMSPro.Services;

namespace WMSPro.Controllers
{
    public class QuanLyKhoController : Controller
    {
        public IActionResult Index() => View(WmsData.DanhSachKho);

        public IActionResult Tao() => View(new Kho { MaKho = WmsData.GenKhoId() });

        [HttpPost]
        public IActionResult Tao(Kho model)
        {
            WmsData.DanhSachKho.Insert(0, model);
            TempData["Success"] = $"Đã thêm kho {model.TenKho}!";
            return RedirectToAction("Index");
        }

        public IActionResult Sua(string id)
        {
            var kho = WmsData.DanhSachKho.FirstOrDefault(x => x.MaKho == id);
            if (kho == null) return NotFound();
            return View(kho);
        }

        [HttpPost]
        public IActionResult Sua(Kho model)
        {
            var idx = WmsData.DanhSachKho.FindIndex(x => x.MaKho == model.MaKho);
            if (idx >= 0) { WmsData.DanhSachKho[idx] = model; TempData["Success"] = $"Đã cập nhật {model.TenKho}!"; }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Xoa(string id)
        {
            var kho = WmsData.DanhSachKho.FirstOrDefault(x => x.MaKho == id);
            if (kho != null) { WmsData.DanhSachKho.Remove(kho); TempData["Success"] = $"Đã xóa {kho.TenKho}!"; }
            return RedirectToAction("Index");
        }
    }
}
