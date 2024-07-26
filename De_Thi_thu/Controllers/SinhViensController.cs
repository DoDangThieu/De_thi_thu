using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using De_Thi_thu.Models;
using Newtonsoft.Json;

namespace De_Thi_thu.Controllers
{
    public class SinhViensController : Controller
    {
        private readonly MyDbContext _context;

        public SinhViensController(MyDbContext context)
        {
            _context = context;
        }

        // GET: SinhViens
        public async Task<IActionResult> Index()
        {
              return _context.SinhViens != null ? 
                          View(await _context.SinhViens.ToListAsync()) :
                          Problem("Entity set 'MyDbContext.SinhViens'  is null.");
        }

        // GET: SinhViens/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.SinhViens == null)
            {
                return NotFound();
            }

            var sinhVien = await _context.SinhViens
                .FirstOrDefaultAsync(m => m.ID == id);
            if (sinhVien == null)
            {
                return NotFound();
            }

            return View(sinhVien);
        }

        // GET: SinhViens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SinhViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Ten,Tuoi,Nganh")] SinhVien sinhVien)
        {
            sinhVien.ID = Guid.NewGuid();
            _context.Add(sinhVien);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: SinhViens/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.SinhViens == null)
            {
                return NotFound();
            }

            var sinhVien = await _context.SinhViens.FindAsync(id);
            if (sinhVien == null)
            {
                return NotFound();
            }
            return View(sinhVien);
        }

        // POST: SinhViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Ten,Tuoi,Nganh")] SinhVien sinhVien)
        {
            if (id != sinhVien.ID)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(sinhVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SinhVienExists(sinhVien.ID))
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

        // GET: SinhViens/Delete/5
        [HttpGet] // nếu k nói gì thì mặc định là htttp get
        public IActionResult Delete(Guid id)
        {
            //lấy ra đối tượng mà muốn xóa
            var deleteSinhVien = _context.SinhViens.Find(id);

            //khi các bạn muốn làm roll back hoặc xem lại giữ liệu đã xóa thì các bạn mới làm đoạn này
            //ép kiểu sang Json để tí nữa xem lại dữ liệu đã xóa và muốn hiển thị ở dạng JSON
            var jsonData = JsonConvert.SerializeObject(deleteSinhVien);
            HttpContext.Session.SetString("deleted", jsonData);// luu giữ lựu đã xóa vào sesion

            //xong khi lưu tạm thời vào session thì mới xóa

            _context.Remove(deleteSinhVien);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> RetrieveDeleteData()
        {
            var jsonData = HttpContext.Session.GetString("deleted"); // lấy dữ liệu đã lưu
            if (jsonData != null)
            {
                var deleteStudent = JsonConvert.DeserializeObject<SinhVien>(jsonData);
                return View("RetrieveDeleteData", deleteStudent);
            }
            else
            {
                // Xử lý khi không tìm thấy dữ liệu trong session
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> RollBack()
        {
            //check xem dữ lieeju đã xóa đã đc lưu vào session hay chưa
            if (HttpContext.Session.Keys.Contains("deleted"))
            {
                //lấy giữ lựu đã lưu vào session ra
                var jSonData = HttpContext.Session.GetString("deleted");

                //tạo 1 đối tượng có dữ liệu y hệt dữ liệu cũ
                var deletedStudent = JsonConvert.DeserializeObject<SinhVien>(jSonData);
                await _context.SinhViens.AddAsync(deletedStudent);  //add lại vào trong db
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            else
            {
                return Content("chưa lưu vào session được xóa");
            }

        }
        private bool SinhVienExists(Guid id)
        {
          return (_context.SinhViens?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
