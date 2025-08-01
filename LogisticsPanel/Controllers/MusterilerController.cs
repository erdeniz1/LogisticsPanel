using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

using LogisticsPanel.Data;
using LogisticsPanel.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class MusterilerController : Controller
{
    private readonly AppDbContext _context;

    public MusterilerController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Musteriler
    public async Task<IActionResult> Index()
    {
        var musteriler = await _context.Musteriler.ToListAsync();
        return View(musteriler);
    }

    // GET: Musteriler/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Musteriler/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Musteri musteri)
    {
        if (ModelState.IsValid)
        {
            _context.Add(musteri);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(musteri);
    }

    // GET: Musteriler/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var musteri = await _context.Musteriler.FindAsync(id);
        if (musteri == null) return NotFound();

        return View(musteri);
    }

    // POST: Musteriler/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Musteri musteri)
    {
        if (id != musteri.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(musteri);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MusteriExists(musteri.Id))
                    return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(musteri);
    }

    // GET: Musteriler/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var musteri = await _context.Musteriler
            .FirstOrDefaultAsync(m => m.Id == id);
        if (musteri == null) return NotFound();

        return View(musteri);
    }

    // POST: Musteriler/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var musteri = await _context.Musteriler.FindAsync(id);
        _context.Musteriler.Remove(musteri);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MusteriExists(int id)
    {
        return _context.Musteriler.Any(e => e.Id == id);
    }
}

