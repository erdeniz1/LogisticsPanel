using LogisticsPanel.Data;
using LogisticsPanel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class RotalarController : Controller
{
    private readonly AppDbContext _context;

    public RotalarController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Rotalar
    public async Task<IActionResult> Index()
    {
        var rotalar = await _context.Rotalar.Include(r => r.Arac).ToListAsync();
        return View(rotalar);
    }

    // GET: Rotalar/Create
    public IActionResult Create()
    {
        ViewBag.Araclar = new SelectList(_context.Araclar, "Id", "Plaka");
        return View();
    }

    // POST: Rotalar/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Rota rota)
    {
        if (ModelState.IsValid)
        {
            _context.Add(rota);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Araclar = new SelectList(_context.Araclar, "Id", "Plaka", rota.AracId);
        return View(rota);
    }

    // GET: Rotalar/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var rota = await _context.Rotalar.FindAsync(id);
        if (rota == null) return NotFound();

        ViewBag.Araclar = new SelectList(_context.Araclar, "Id", "Plaka", rota.AracId);
        return View(rota);
    }

    // POST: Rotalar/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Rota rota)
    {
        if (id != rota.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(rota);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Rotalar.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Araclar = new SelectList(_context.Araclar, "Id", "Plaka", rota.AracId);
        return View(rota);
    }

    // GET: Rotalar/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var rota = await _context.Rotalar
            .Include(r => r.Arac)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (rota == null) return NotFound();

        return View(rota);
    }

    // POST: Rotalar/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var rota = await _context.Rotalar.FindAsync(id);
        _context.Rotalar.Remove(rota);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
