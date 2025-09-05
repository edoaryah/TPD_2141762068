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
          .Include(s => s.KategoriSurat) // Include data Kategori
          .AsQueryable();

      if (!string.IsNullOrEmpty(searchQuery))
      {
        query = query.Where(s => s.Judul.Contains(searchQuery));
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
        KategoriSuratId = dto.KategoriSuratId,
        WaktuPengarsipan = DateTime.Now,
        FilePath = filePath
      };

      _context.ArsipSurats.Add(arsipSurat);
      await _context.SaveChangesAsync();
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
