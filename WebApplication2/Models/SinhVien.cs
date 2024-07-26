using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class SinhVien
    {
        public Guid ID { get; set; }
        public string Ten { get; set; }
        public int Tuoi { get; set; }
        public string Nganh { get; set; }
    }
}
