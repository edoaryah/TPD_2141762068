using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcFull.Models
{
  public class KategoriSurat
  {
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Nama Kategori wajib diisi.")]
    [StringLength(100)]
    public string NamaKategori { get; set; }

    [Required(ErrorMessage = "Keterangan wajib diisi.")]
    [StringLength(255)]
    public string Keterangan { get; set; }

    // Soft Delete Kategori
    public bool IsDeleted { get; set; }

    // Relasi one-to-many: Satu kategori bisa memiliki banyak surat
    public ICollection<ArsipSurat> ArsipSurats { get; set; }
  }
}
