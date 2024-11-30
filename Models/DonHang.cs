using System.ComponentModel.DataAnnotations;
using asm2.Models;

namespace asm2.Models
{
    public class DonHang
    {
        [Key]
        public string MaDonHang { get; set; }
        public string TenDonHang { get; set; }
        public DateTime NgayDatHang { get; set; }
        public Guid KhachHangID { get; set; } 
        public KhachHang KhachHang { get; set; } 
    }
}

