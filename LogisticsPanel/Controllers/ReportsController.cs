using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogisticsPanel.Data;
using LogisticsPanel.ViewModels;  // ViewModel namespace'i ekle

public class ReportsController : Controller
{
    private readonly AppDbContext _context;

    public ReportsController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var gonderiVeri = await _context.Gonderiler
            .GroupBy(g => g.TahminiTeslimTarihi.Date)
            .Select(g => new
            {
                Tarih = g.Key,
                Sayisi = g.Count()
            })
            .OrderBy(x => x.Tarih)
            .ToListAsync();

        var gonderiGunluk = gonderiVeri
            .Select(g => new GonderiGunlukDto
            {
                Tarih = g.Tarih.ToString("yyyy-MM-dd"),
                Sayisi = g.Sayisi
            })
            .OrderBy(g => g.Tarih)
            .ToList();

        var enCokTeslim = await _context.Gonderiler
            .Where(g => g.Durum == "Teslim Edildi" && g.AtananArac != null)
            .GroupBy(g => g.AtananArac.Plaka)
            .Select(g => new EnCokTeslimDto
            {
                Arac = g.Key,
                TeslimatSayisi = g.Count()
            })
            .OrderByDescending(x => x.TeslimatSayisi)
            .FirstOrDefaultAsync();

        int toplam = await _context.Gonderiler.CountAsync();
        int teslim = await _context.Gonderiler.CountAsync(g => g.Durum == "Teslim Edildi");
        double basariOrani = toplam > 0 ? (double)teslim / toplam * 100 : 0;

        var model = new ReportViewModel
        {
            GunlukGonderiler = gonderiGunluk,
            EnCokTeslimArac = enCokTeslim,
            BasariOrani = basariOrani
        };

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ExportPdf()
    {
        var veriler = await _context.Gonderiler
            .Include(g => g.GonderenMusteri)
            .Include(g => g.AtananArac)
            .ToListAsync();

        return new Rotativa.AspNetCore.ViewAsPdf("PdfReport", veriler)
        {
            FileName = "Rapor.pdf"
        };
    }

    public IActionResult PdfReport(List<LogisticsPanel.Models.Gonderi> veriler)
    {
        return View(veriler);
    }
}
