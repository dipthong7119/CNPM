using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoMVC.Models;

namespace QuanLyKhoMVC.Controllers
{
    public class VatTusController : Controller
    {
        private readonly AppDbContext _context;

        public VatTusController(AppDbContext context)
        {
            _context = context;
        }

        // GET: VatTus
        public async Task<IActionResult> Index()
        {
            return View(await _context.DanhSachVatTu.ToListAsync());
        }

        // GET: VatTus/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vatTu = await _context.DanhSachVatTu
                .FirstOrDefaultAsync(m => m.MaVatTu == id);
            if (vatTu == null)
            {
                return NotFound();
            }

            return View(vatTu);
        }

        // GET: VatTus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VatTus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Tìm dòng này trong file VatTusController.cs
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaVatTu,TenVatTu,SoLuong,ViTri")] VatTu vatTu)
        {
            if (ModelState.IsValid) // Kiểm tra xem dữ liệu nhập vào có đúng kiểu không
            {
                // Kiểm tra thêm điều kiện thực tế (Vibe nghiệp vụ)
                if (vatTu.SoLuong < 0)
                {
                    ModelState.AddModelError("SoLuong", "Số lượng không được nhỏ hơn 0 bạn nhé!");
                    return View(vatTu);
                }

                _context.Add(vatTu); // Thêm vào danh sách chờ
                await _context.SaveChangesAsync(); // Lưu thực sự vào SQL Server
                return RedirectToAction(nameof(Index)); // Lưu xong thì quay về trang danh sách
            }
            return View(vatTu);
        }
        // GET: VatTus/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vatTu = await _context.DanhSachVatTu.FindAsync(id);
            if (vatTu == null)
            {
                return NotFound();
            }
            return View(vatTu);
        }

        // POST: VatTus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaVatTu,TenVatTu,SoLuong,ViTri")] VatTu vatTu)
        {
            if (id != vatTu.MaVatTu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vatTu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VatTuExists(vatTu.MaVatTu))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vatTu);
        }

        // GET: VatTus/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vatTu = await _context.DanhSachVatTu
                .FirstOrDefaultAsync(m => m.MaVatTu == id);
            if (vatTu == null)
            {
                return NotFound();
            }

            return View(vatTu);
        }

        // POST: VatTus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var vatTu = await _context.DanhSachVatTu.FindAsync(id);
            if (vatTu != null)
            {
                _context.DanhSachVatTu.Remove(vatTu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VatTuExists(string id)
        {
            return _context.DanhSachVatTu.Any(e => e.MaVatTu == id);
        }
    }
}
