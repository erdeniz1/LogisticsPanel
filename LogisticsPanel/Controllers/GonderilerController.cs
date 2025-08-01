using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogisticsPanel.Models;
using System.Threading.Tasks;
using System.Linq;
using System;
using LogisticsPanel.Data;

namespace LogisticsPanel.Controllers
{
    public class GonderilerController : Controller
    {
        private readonly AppDbContext _context;

        public GonderilerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Gonderiler
        public async Task<IActionResult> Index(string durumFilter = null)
        {
            var query = _context.Gonderiler.Include(g => g.GonderenMusteri).AsQueryable();

            if (!string.IsNullOrEmpty(durumFilter))
            {
                query = query.Where(g => g.Durum == durumFilter);
            }

            var gonderiler = await query
     .Select(g => new Gonderi
     {
         Id = g.Id,
         GonderiNo = g.GonderiNo,
         MusteriId = g.MusteriId,
         GonderenMusteri = g.GonderenMusteri,
         AliciAdresi = g.AliciAdresi,
         Durum = g.Durum,
         TahminiTeslimTarihi = g.TahminiTeslimTarihi,
         AtananAracId = g.AtananAracId,
         AtananArac = g.AtananArac
     })
     .OrderByDescending(g => g.Id)
     .ToListAsync();

            ViewBag.Durumlar = new[] { "Hazırlanıyor", "Yolda", "Teslim Edildi", "İptal" };
            ViewBag.DurumFilter = durumFilter;

            return View(gonderiler);
        }

        // GET: Gonderiler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var gonderi = await _context.Gonderiler
                .Include(g => g.GonderenMusteri)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (gonderi == null) return NotFound();

            return View(gonderi);
        }

        // GET: Gonderiler/Create
        public IActionResult Create()
        {
            ViewBag.Musteriler = _context.Musteriler.ToList();
            ViewBag.Durumlar = new[] { "Hazırlanıyor", "Yolda", "Teslim Edildi", "İptal" };
            return View();
        }

        // POST: Gonderiler/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Gonderi gonderi)
        {
            if (ModelState.IsValid)
            {
                gonderi.GonderiNo = GenerateGonderiNo();
                _context.Add(gonderi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Musteriler = _context.Musteriler.ToList();
            ViewBag.Durumlar = new[] { "Hazırlanıyor", "Yolda", "Teslim Edildi", "İptal" };
            return View(gonderi);
        }

        // GET: Gonderiler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var gonderi = await _context.Gonderiler.FindAsync(id);
            if (gonderi == null) return NotFound();

            ViewBag.Musteriler = _context.Musteriler.ToList();
            ViewBag.Durumlar = new[] { "Hazırlanıyor", "Yolda", "Teslim Edildi", "İptal" };
            return View(gonderi);
        }

        // POST: Gonderiler/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Gonderi gonderi)
        {
            if (id != gonderi.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gonderi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GonderiExists(gonderi.Id))
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

            ViewBag.Musteriler = _context.Musteriler.ToList();
            ViewBag.Durumlar = new[] { "Hazırlanıyor", "Yolda", "Teslim Edildi", "İptal" };
            return View(gonderi);
        }

        // GET: Gonderiler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var gonderi = await _context.Gonderiler
                .Include(g => g.GonderenMusteri)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (gonderi == null) return NotFound();

            return View(gonderi);
        }

        // POST: Gonderiler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gonderi = await _context.Gonderiler.FindAsync(id);
            if (gonderi != null)
            {
                _context.Gonderiler.Remove(gonderi);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool GonderiExists(int id)
        {
            return _context.Gonderiler.Any(e => e.Id == id);
        }

        private string GenerateGonderiNo()
        {
            return "GND" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}
