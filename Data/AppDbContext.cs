using Microsoft.EntityFrameworkCore;
using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<KategoriSurat> KategoriSurats { get; set; }
    public DbSet<ArsipSurat> ArsipSurats { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Seeding data awal untuk kategori (opsional, tapi membantu untuk testing)
      modelBuilder.Entity<KategoriSurat>().HasData(
          new KategoriSurat { Id = 1, NamaKategori = "Undangan", Keterangan = "Surat bersifat undangan resmi." },
          new KategoriSurat { Id = 2, NamaKategori = "Pengumuman", Keterangan = "Surat bersifat pengumuman." },
          new KategoriSurat { Id = 3, NamaKategori = "Nota Dinas", Keterangan = "Surat internal antar dinas." },
          new KategoriSurat { Id = 4, NamaKategori = "Pemberitahuan", Keterangan = "Surat bersifat pemberitahuan informasi." }
      );
    }
  }
}
