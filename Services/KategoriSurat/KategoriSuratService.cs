using Microsoft.EntityFrameworkCore;
using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.DTOs;

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
      var query = _context.KategoriSurats.Where(k => !k.IsDeleted).AsQueryable();

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
      // validasi untuk prevent nama duplikat pada kategori yg aktif
      var existingCategory = await _context.KategoriSurats
          .FirstOrDefaultAsync(k => k.NamaKategori.ToLower() == dto.NamaKategori.ToLower() && !k.IsDeleted);

      if (existingCategory != null)
      {
        throw new InvalidOperationException("Kategori dengan nama yang sama sudah ada.");
      }

      var kategori = new Models.KategoriSurat
      {
        NamaKategori = dto.NamaKategori,
        Keterangan = dto.Keterangan,
        IsDeleted = false // Pastikan IsDeleted = false saat dibuat
      };

      _context.KategoriSurats.Add(kategori);
      await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(KategoriSuratDto dto)
    {
      // validasi untuk prevent nama duplikat saat update
      var existingCategory = await _context.KategoriSurats
          .FirstOrDefaultAsync(k => k.NamaKategori.ToLower() == dto.NamaKategori.ToLower() && !k.IsDeleted && k.Id != dto.Id);

      if (existingCategory != null)
      {
        throw new InvalidOperationException("Kategori dengan nama yang sama sudah ada.");
      }

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
        // menerapkan soft delete
        kategori.IsDeleted = true;
        _context.KategoriSurats.Update(kategori);
        await _context.SaveChangesAsync();
      }
    }
  }
}
