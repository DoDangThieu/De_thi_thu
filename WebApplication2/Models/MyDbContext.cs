using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        {
            
        }
        public MyDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ADMIN\\SQLEXPRESS;Database=Test02;Trusted_Connection=True;TrustServerCertificate=True");
        }
        public DbSet<SinhVien> SinhViens { get; set; }
        public DbSet<LopHoc> LopHocs { get; set; }
    }
}
