using System.ComponentModel.DataAnnotations;

namespace asm2.Models
{
    public class KhachHang
    {
        [Key]
        public Guid ID { get; set; }
        public string HoTen { get; set; }
        public int Tuoi { get; set; }
        public string DiaChi { get; set; }
    }
}
