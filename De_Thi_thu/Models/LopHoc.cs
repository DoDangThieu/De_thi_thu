using System.ComponentModel.DataAnnotations;

namespace De_Thi_thu.Models
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
