using Microsoft.AspNetCore.Mvc;
using WMSPro.Services;

namespace WMSPro.Controllers
{
    public class TonKhoController : Controller
    {
        public IActionResult Index(string? filter, string? q)
        {
            ViewBag.Filter = filter ?? "tat-ca";
            ViewBag.Q = q ?? "";
            var ds = WmsData.DanhSachVatTu.AsQueryable();
            if (filter == "sap-het") ds = ds.Where(x => x.SoLuongTon <= x.MucTonToiThieu);
            if (!string.IsNullOrEmpty(q))
                ds = ds.Where(x => x.TenVatTu.Contains(q, StringComparison.OrdinalIgnoreCase)
                    || x.MaVatTu.Contains(q, StringComparison.OrdinalIgnoreCase));
            return View(ds.ToList());
        }
    }

    public class CanhBaoController : Controller
    {
        public IActionResult Index()
        {
            // Thuật toán cảnh báo tồn kho thấp
            var cbSapHet = WmsData.DanhSachVatTu
                .Where(v => v.SoLuongTon <= v.MucTonToiThieu)
                .Select(v => new WMSPro.Models.CanhBaoItem
                {
                    TenHang = $"{v.TenVatTu} — Sắp hết hàng",
                    MoTa = $"Tồn kho: {v.SoLuongTon} / Mức tối thiểu: {v.MucTonToiThieu} đơn vị · Vị trí: {v.MaViTri} · Lô: {v.MaLo}",
                    ThoiGian = "Vừa cập nhật",
                    Loai = "sap-het"
                }).ToList();
            var cbTonLau = WmsData.DanhSachCanhBao.Where(c => c.Loai == "ton-lau").ToList();
            ViewBag.CbSapHet = cbSapHet;
            ViewBag.CbTonLau = cbTonLau;
            return View();
        }
    }

    public class BaoCaoController : Controller
    {
        public IActionResult Index() => View(WmsData.DanhSachBaoCao);
    }
}
