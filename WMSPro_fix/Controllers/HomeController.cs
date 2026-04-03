using Microsoft.AspNetCore.Mvc;
using WMSPro.Models;
using WMSPro.Services;

namespace WMSPro.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // T?o danh sách ho?t ??ng g?n ?ây t? c? phi?u nh?p và xu?t
            var hoatDongGanDay = new List<HoatDongItem>();
            
            // Thêm phi?u nh?p
            foreach (var pn in WmsData.DanhSachPhieuNhap)
            {
                hoatDongGanDay.Add(new HoatDongItem
                {
                    SoPhieu = pn.SoPhieu,
                    LoaiPhieu = "Nhập",
                    TenHang = pn.TenHang,
                    SoLuong = pn.SlThucTe,
                    TrangThai = pn.TrangThai
                });
            }
            
            // Thêm phi?u xu?t
            foreach (var px in WmsData.DanhSachPhieuXuat)
            {
                hoatDongGanDay.Add(new HoatDongItem
                {
                    SoPhieu = px.SoPhieu,
                    LoaiPhieu = "Xuất",
                    TenHang = px.TenHang,
                    SoLuong = -px.SlXuat, // Âm ?? hi?n th? xu?t
                    TrangThai = px.TrangThai
                });
            }
            
            // S?p x?p theo s? phi?u gi?m d?n (m?i nh?t tr??c)
            hoatDongGanDay = hoatDongGanDay.OrderByDescending(x => x.SoPhieu).ToList();

            var vm = new DashboardViewModel
            {
                TongTonKho = WmsData.DanhSachVatTu.Sum(v => v.SoLuongTon),
                NhapKhoHomNay = 248,
                XuatKhoHomNay = 186,
                SoCanhBao = WmsData.DanhSachVatTu.Count(v => v.SoLuongTon <= v.MucTonToiThieu),
                HoatDongGanDay = hoatDongGanDay,
                DanhSachTonKho = WmsData.DanhSachVatTu,
                DanhSachCanhBao = WmsData.DanhSachCanhBao
            };
            return View(vm);
        }
    }
}
