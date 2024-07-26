using Microsoft.EntityFrameworkCore;

namespace De_Thi_thu.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ADMIN\\SQLEXPRESS;Database=Test03;Trusted_Connection=True;TrustServerCertificate=True");
        }
        public DbSet<SinhVien> SinhViens { get; set; }
        public DbSet<LopHoc> LopHocs {  get; set; }
    }
}
