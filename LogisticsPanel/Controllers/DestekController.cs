using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogisticsPanel.Data;
using LogisticsPanel.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

public class DestekController : Controller
{
    private readonly AppDbContext _context;

    public DestekController(AppDbContext context)
    {
        _context = context;
    }

    // Listeleme (Admin ve Müşteri farklı gösterim olabilir)
    public async Task<IActionResult> Index()
    {
        var destekler = await _context.Destekler.Include(d => d.Musteri).OrderByDescending(d => d.Tarih).ToListAsync();
        return View(destekler);
    }

    // Yeni destek talebi formu
    public IActionResult Create()
    {
        ViewBag.Musteriler = new SelectList(_context.Musteriler, "Id", "Ad");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Destek destek)
    {
        if (ModelState.IsValid)
        {
            _context.Destekler.Add(destek);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Musteriler = new SelectList(_context.Musteriler, "Id", "Ad", destek.MusteriId);
        return View(destek);
    }

    // Detay sayfası
    public async Task<IActionResult> Details(int id)
    {
        var destek = await _context.Destekler.Include(d => d.Musteri).FirstOrDefaultAsync(d => d.Id == id);
        if (destek == null) return NotFound();
        return View(destek);
    }

    // Düzenleme (Admin kullanımı)
    public async Task<IActionResult> Edit(int id)
    {
        var destek = await _context.Destekler.FindAsync(id);
        if (destek == null) return NotFound();
        ViewBag.Musteriler = new SelectList(_context.Musteriler, "Id", "Ad", destek.MusteriId);
        return View(destek);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Destek destek)
    {
        if (ModelState.IsValid)
        {
            _context.Update(destek);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Musteriler = new SelectList(_context.Musteriler, "Id", "Ad", destek.MusteriId);
        return View(destek);
    }

    // Silme
    public async Task<IActionResult> Delete(int id)
    {
        var destek = await _context.Destekler.FindAsync(id);
        if (destek == null) return NotFound();
        return View(destek);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var destek = await _context.Destekler.FindAsync(id);
        if (destek != null)
        {
            _context.Destekler.Remove(destek);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
