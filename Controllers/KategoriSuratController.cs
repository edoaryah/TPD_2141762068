using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AspnetCoreMvcFull.DTOs;
using AspnetCoreMvcFull.Services.KategoriSurat;

namespace AspnetCoreMvcFull.Controllers
{
  public class KategoriSuratController : Controller
  {
    private readonly IKategoriSuratService _kategoriService;

    public KategoriSuratController(IKategoriSuratService kategoriService)
    {
      _kategoriService = kategoriService;
    }

    // GET: /KategoriSurat
    public async Task<IActionResult> Index(string searchQuery)
    {
      ViewData["CurrentFilter"] = searchQuery;
      var kategoriList = await _kategoriService.GetAllAsync(searchQuery);
      return View(kategoriList);
    }

    // GET: /KategoriSurat/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: /KategoriSurat/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(KategoriSuratDto dto)
    {
      if (ModelState.IsValid)
      {
        await _kategoriService.CreateAsync(dto);
        TempData["SuccessMessage"] = "Kategori baru berhasil ditambahkan!";
        return RedirectToAction(nameof(Index));
      }
      return View(dto);
    }

    // GET: /KategoriSurat/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null) return NotFound();

      var kategori = await _kategoriService.GetByIdAsync(id.Value);
      if (kategori == null) return NotFound();

      var dto = new KategoriSuratDto
      {
        Id = kategori.Id,
        NamaKategori = kategori.NamaKategori,
        Keterangan = kategori.Keterangan
      };

      return View("Create", dto);
    }

    // POST: /KategoriSurat/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, KategoriSuratDto dto)
    {
      if (id != dto.Id) return NotFound();

      if (ModelState.IsValid)
      {
        await _kategoriService.UpdateAsync(dto);
        TempData["SuccessMessage"] = "Kategori berhasil diperbarui!";
        return RedirectToAction(nameof(Index));
      }
      return View(dto);
    }

    // POST: /KategoriSurat/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      await _kategoriService.DeleteAsync(id);
      TempData["SuccessMessage"] = "Kategori berhasil dihapus!";
      return RedirectToAction(nameof(Index));
    }
  }
}
