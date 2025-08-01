using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogisticsPanel.Data;
using LogisticsPanel.Models;

public class AraclarController : Controller
{
    private readonly AppDbContext _context;

    public AraclarController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Arac
    public async Task<IActionResult> Index()
    {
        var araclar = await _context.Araclar.ToListAsync();
        return View(araclar);
    }

    // GET: Arac/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Arac/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Arac arac)
    {
        if (ModelState.IsValid)
        {
            _context.Add(arac);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(arac);
    }

    // GET: Arac/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var arac = await _context.Araclar.FindAsync(id);
        if (arac == null) return NotFound();

        return View(arac);
    }

    // POST: Arac/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Arac arac)
    {
        if (id != arac.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(arac);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AracExists(arac.Id))
                    return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(arac);
    }

    // GET: Arac/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var arac = await _context.Araclar
            .FirstOrDefaultAsync(m => m.Id == id);
        if (arac == null) return NotFound();

        return View(arac);
    }

    // POST: Arac/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var arac = await _context.Araclar.FindAsync(id);
        _context.Araclar.Remove(arac);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool AracExists(int id)
    {
        return _context.Araclar.Any(e => e.Id == id);
    }
}
