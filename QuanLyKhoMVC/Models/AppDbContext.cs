using Microsoft.EntityFrameworkCore;

namespace QuanLyKhoMVC.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<VatTu> DanhSachVatTu { get; set; }
    }
}