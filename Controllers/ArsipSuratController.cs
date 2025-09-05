using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Threading.Tasks;
using AspnetCoreMvcFull.DTOs;
using AspnetCoreMvcFull.Services.ArsipSurat;
using AspnetCoreMvcFull.Services.KategoriSurat;

namespace AspnetCoreMvcFull.Controllers
{
  public class ArsipSuratController : Controller
  {
    private readonly IArsipSuratService _arsipService;
    private readonly IKategoriSuratService _kategoriService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ArsipSuratController(IArsipSuratService arsipService, IKategoriSuratService kategoriService, IWebHostEnvironment webHostEnvironment)
    {
      _arsipService = arsipService;
      _kategoriService = kategoriService;
      _webHostEnvironment = webHostEnvironment;
    }

    // GET: /ArsipSurat atau /
    public async Task<IActionResult> Index(string searchQuery)
    {
      ViewData["CurrentFilter"] = searchQuery;
      var arsipList = await _arsipService.GetAllAsync(searchQuery);
      return View(arsipList);
    }

    // GET: /ArsipSurat/Detail/5
    public async Task<IActionResult> Detail(int? id)
    {
      if (id == null) return NotFound();

      var arsip = await _arsipService.GetByIdAsync(id.Value);
      if (arsip == null) return NotFound();

      return View(arsip);
    }

    // GET: /ArsipSurat/Create
    public async Task<IActionResult> Create()
    {
      // Siapkan data kategori untuk dropdown
      ViewData["KategoriList"] = new SelectList(await _kategoriService.GetAllAsync(null), "Id", "NamaKategori");
      // Kirim DTO kosong ke View agar tidak null
      return View(new ArsipSuratDto());
    }

    // POST: /ArsipSurat/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ArsipSuratDto dto)
    {
      if (ModelState.IsValid)
      {
        await _arsipService.CreateAsync(dto);
        // Pesan untuk ditampilkan di halaman utama setelah redirect
        TempData["SuccessMessage"] = "Data berhasil disimpan!";
        return RedirectToAction(nameof(Index));
      }
      // Jika model tidak valid, kirim kembali daftar kategori ke view
      ViewData["KategoriList"] = new SelectList(await _kategoriService.GetAllAsync(null), "Id", "NamaKategori");
      return View(dto);
    }

    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null) return NotFound();

      var arsip = await _arsipService.GetByIdAsync(id.Value);
      if (arsip == null) return NotFound();

      // Mapping dari Model ke DTO untuk dikirim ke View
      var dto = new ArsipSuratDto
      {
        Id = arsip.Id,
        NomorSurat = arsip.NomorSurat,
        Judul = arsip.Judul,
        KategoriSuratId = arsip.KategoriSuratId
      };

      ViewData["KategoriList"] = new SelectList(await _kategoriService.GetAllAsync(null), "Id", "NamaKategori", arsip.KategoriSuratId);
      return View("Create", dto); // Menggunakan kembali View "Create.cshtml"
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ArsipSuratDto dto)
    {
      if (id != dto.Id) return NotFound();

      // Hapus validasi untuk FilePdf jika tidak ada file baru yang diunggah
      if (dto.FilePdf == null)
      {
        ModelState.Remove("FilePdf");
      }

      if (ModelState.IsValid)
      {
        await _arsipService.UpdateAsync(dto);
        TempData["SuccessMessage"] = "Data berhasil diperbarui!";
        return RedirectToAction(nameof(Index));
      }

      ViewData["KategoriList"] = new SelectList(await _kategoriService.GetAllAsync(null), "Id", "NamaKategori", dto.KategoriSuratId);
      return View("Create", dto); // Kembali ke form jika tidak valid
    }

    // POST: /ArsipSurat/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      await _arsipService.DeleteAsync(id);
      TempData["SuccessMessage"] = "Data berhasil dihapus!";
      return RedirectToAction(nameof(Index));
    }

    // GET: /ArsipSurat/Download/5
    public async Task<IActionResult> Download(int? id)
    {
      if (id == null) return NotFound();

      var arsip = await _arsipService.GetByIdAsync(id.Value);
      if (arsip == null || string.IsNullOrEmpty(arsip.FilePath)) return NotFound();

      var memory = new MemoryStream();
      var path = Path.Combine(_webHostEnvironment.WebRootPath, arsip.FilePath);

      using (var stream = new FileStream(path, FileMode.Open))
      {
        await stream.CopyToAsync(memory);
      }
      memory.Position = 0;

      // Memberi nama file saat di-download
      var downloadFileName = $"{arsip.NomorSurat.Replace("/", "_")}_{arsip.Judul}.pdf";

      return File(memory, "application/pdf", downloadFileName);
    }
  }
}
