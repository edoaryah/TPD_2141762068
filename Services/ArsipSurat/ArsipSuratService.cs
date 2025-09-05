using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.DTOs;
using AspnetCoreMvcFull.Services.Storage;

namespace AspnetCoreMvcFull.Services.ArsipSurat
{
  public class ArsipSuratService : IArsipSuratService
  {
    private readonly AppDbContext _context;
    private readonly IFileStorageService _fileStorageService;

    public ArsipSuratService(AppDbContext context, IFileStorageService fileStorageService)
    {
      _context = context;
      _fileStorageService = fileStorageService;
    }

    public async Task<IEnumerable<Models.ArsipSurat>> GetAllAsync(string searchQuery)
    {
      var query = _context.ArsipSurats
          .Include(s => s.KategoriSurat)
          .AsQueryable();

      if (!string.IsNullOrEmpty(searchQuery))
      {
        // Ubah searchQuery menjadi huruf kecil sekali saja untuk efisiensi
        var lowerCaseQuery = searchQuery.ToLower();

        // Ubah kondisi Where untuk mencari di dua kolom dan abaikan case
        query = query.Where(s =>
            s.Judul.ToLower().Contains(lowerCaseQuery) ||
            s.NomorSurat.ToLower().Contains(lowerCaseQuery)
        );
      }

      return await query.OrderByDescending(s => s.WaktuPengarsipan).ToListAsync();
    }

    public async Task<Models.ArsipSurat> GetByIdAsync(int id)
    {
      return await _context.ArsipSurats
          .Include(s => s.KategoriSurat)
          .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task CreateAsync(ArsipSuratDto dto)
    {
      // Simpan file PDF ke storage
      var filePath = await _fileStorageService.SaveFileAsync(dto.FilePdf, "surat");

      var arsipSurat = new Models.ArsipSurat
      {
        NomorSurat = dto.NomorSurat,
        Judul = dto.Judul,
        KategoriSuratId = dto.KategoriSuratId.Value,
        // WaktuPengarsipan = DateTime.Now,
        WaktuPengarsipan = DateTime.UtcNow,
        FilePath = filePath
      };

      _context.ArsipSurats.Add(arsipSurat);
      await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ArsipSuratDto dto)
    {
      var arsipSurat = await GetByIdAsync(dto.Id);
      if (arsipSurat != null)
      {
        // Cek apakah ada file baru yang diunggah
        if (dto.FilePdf != null && dto.FilePdf.Length > 0)
        {
          // 1. Hapus file lama
          _fileStorageService.DeleteFile(arsipSurat.FilePath);

          // 2. Simpan file baru
          var newFilePath = await _fileStorageService.SaveFileAsync(dto.FilePdf, "surat");
          arsipSurat.FilePath = newFilePath;
        }

        // Update properti lainnya
        arsipSurat.NomorSurat = dto.NomorSurat;
        arsipSurat.Judul = dto.Judul;
        arsipSurat.KategoriSuratId = dto.KategoriSuratId.Value;

        _context.ArsipSurats.Update(arsipSurat);
        await _context.SaveChangesAsync();
      }
    }

    public async Task DeleteAsync(int id)
    {
      var arsipSurat = await GetByIdAsync(id);
      if (arsipSurat != null)
      {
        // Hapus file fisik terlebih dahulu
        _fileStorageService.DeleteFile(arsipSurat.FilePath);

        // Hapus data dari database
        _context.ArsipSurats.Remove(arsipSurat);
        await _context.SaveChangesAsync();
      }
    }
  }
}
