using Microsoft.EntityFrameworkCore;

namespace asm2.Models
{
    public class as2mDBContext : DbContext
    {
        public as2mDBContext(DbContextOptions<as2mDBContext> options) : base(options)
        {
        }

        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}