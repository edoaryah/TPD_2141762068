using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcFull.DTOs
{
  public class ArsipSuratDto
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "Nomor Surat wajib diisi.")]
    public string NomorSurat { get; set; }

    [Required(ErrorMessage = "Judul Surat wajib diisi.")]
    public string Judul { get; set; }

    [Required(ErrorMessage = "Kategori wajib dipilih.")]
    public int? KategoriSuratId { get; set; }

    [Required(ErrorMessage = "File PDF wajib diunggah.")]
    public IFormFile? FilePdf { get; set; }
  }
}
