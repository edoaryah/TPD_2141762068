using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.DTOs;
using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.Services.KategoriSurat
{
  public class KategoriSuratService : IKategoriSuratService
  {
    private readonly AppDbContext _context;

    public KategoriSuratService(AppDbContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Models.KategoriSurat>> GetAllAsync(string searchQuery)
    {
      var query = _context.KategoriSurats.AsQueryable();

      if (!string.IsNullOrEmpty(searchQuery))
      {
        var lowerCaseQuery = searchQuery.ToLower();
        query = query.Where(k =>
            k.NamaKategori.ToLower().Contains(lowerCaseQuery) ||
            k.Keterangan.ToLower().Contains(lowerCaseQuery)
        );
      }

      return await query.OrderBy(k => k.Id).ToListAsync();
    }

    public async Task<Models.KategoriSurat> GetByIdAsync(int id)
    {
      return await _context.KategoriSurats.FindAsync(id);
    }

    public async Task CreateAsync(KategoriSuratDto dto)
    {
      var kategori = new Models.KategoriSurat
      {
        NamaKategori = dto.NamaKategori,
        Keterangan = dto.Keterangan
      };
      _context.KategoriSurats.Add(kategori);
      await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(KategoriSuratDto dto)
    {
      var kategori = await GetByIdAsync(dto.Id);
      if (kategori != null)
      {
        kategori.NamaKategori = dto.NamaKategori;
        kategori.Keterangan = dto.Keterangan;
        _context.KategoriSurats.Update(kategori);
        await _context.SaveChangesAsync();
      }
    }

    public async Task DeleteAsync(int id)
    {
      var kategori = await GetByIdAsync(id);
      if (kategori != null)
      {
        _context.KategoriSurats.Remove(kategori);
        await _context.SaveChangesAsync();
      }
    }
  }
}
