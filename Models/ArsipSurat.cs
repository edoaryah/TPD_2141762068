using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models
{
  public class ArsipSurat
  {
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Nomor Surat wajib diisi.")]
    [StringLength(50)]
    public string NomorSurat { get; set; }

    [Required(ErrorMessage = "Judul Surat wajib diisi.")]
    [StringLength(200)]
    public string Judul { get; set; }

    [Required]
    public DateTime WaktuPengarsipan { get; set; }

    [Required(ErrorMessage = "File PDF wajib diunggah.")]
    public string FilePath { get; set; } // Path relatif ke file PDF yang disimpan

    // Foreign Key untuk KategoriSurat
    [Required(ErrorMessage = "Kategori wajib dipilih.")]
    public int KategoriSuratId { get; set; }

    // Navigation Property (Relasi many-to-one)
    [ForeignKey("KategoriSuratId")]
    public KategoriSurat KategoriSurat { get; set; }
  }
}
