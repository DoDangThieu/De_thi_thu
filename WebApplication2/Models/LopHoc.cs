using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class LopHoc
    {
        [Key]
        public string MaLop { get; set; }
        public string TenLop { get; set; }
        public int Khoa { get; set; }
        public virtual ICollection<SinhVien> SinhViens { get; set; }
    }
}
