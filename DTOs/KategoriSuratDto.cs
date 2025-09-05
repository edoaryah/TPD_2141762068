using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcFull.DTOs
{
  public class KategoriSuratDto
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "Nama Kategori wajib diisi.")]
    public string NamaKategori { get; set; }

    [Required(ErrorMessage = "Keterangan wajib diisi.")]
    public string Keterangan { get; set; }
  }
}
