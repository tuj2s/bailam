using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using asm2.Models;

namespace tu_51549.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly as2mDBContext _context;

        public KhachHangController(as2mDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var khachHangs = _context.KhachHangs.ToList();
            return View(khachHangs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                khachHang.ID = Guid.NewGuid(); 
                _context.KhachHangs.Add(khachHang);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(khachHang);
        }

        public IActionResult Edit(Guid id)
        {
            var khachHang = _context.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetString("OriginalKhachHang", JsonConvert.SerializeObject(khachHang));
            return View(khachHang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, KhachHang khachHang)
        {
            if (id != khachHang.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khachHang);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.KhachHangs.Any(e => e.ID == id))
                    {
                        return NotFound();
                    }
                    ModelState.AddModelError("", "Unable to save changes. The record was modified by another user.");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(khachHang);
        }

        public IActionResult Delete(Guid id)
        {
            var khachHang = _context.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var khachHang = _context.KhachHangs.Find(id);
            if (khachHang != null)
            {
                _context.KhachHangs.Remove(khachHang);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Rollback(Guid id)
        {
            var originalKhachHangJson = HttpContext.Session.GetString("OriginalKhachHang");
            if (string.IsNullOrEmpty(originalKhachHangJson))
            {
                ModelState.AddModelError("", "No rollback data available.");
                return RedirectToAction(nameof(Index));
            }

            var originalKhachHang = JsonConvert.DeserializeObject<KhachHang>(originalKhachHangJson);
            if (originalKhachHang == null || originalKhachHang.ID != id)
            {
                ModelState.AddModelError("", "Invalid rollback data.");
                return RedirectToAction(nameof(Index));
            }

            _context.KhachHangs.Update(originalKhachHang);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
