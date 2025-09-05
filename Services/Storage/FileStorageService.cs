using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Services.Storage
{
  public class FileStorageService : IFileStorageService
  {
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileStorageService(IWebHostEnvironment webHostEnvironment)
    {
      _webHostEnvironment = webHostEnvironment;
    }

    public async Task<string> SaveFileAsync(IFormFile file, string subfolder)
    {
      if (file == null || file.Length == 0)
      {
        return null;
      }

      var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", subfolder);

      if (!Directory.Exists(uploadsFolder))
      {
        Directory.CreateDirectory(uploadsFolder);
      }

      // Membuat nama file unik untuk menghindari konflik
      var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
      var filePath = Path.Combine(uploadsFolder, uniqueFileName);

      using (var stream = new FileStream(filePath, FileMode.Create))
      {
        await file.CopyToAsync(stream);
      }

      // Mengembalikan path relatif untuk disimpan di database
      return Path.Combine("uploads", subfolder, uniqueFileName).Replace("\\", "/");
    }

    public void DeleteFile(string relativePath)
    {
      if (string.IsNullOrEmpty(relativePath)) return;

      var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, relativePath);
      if (File.Exists(fullPath))
      {
        File.Delete(fullPath);
      }
    }
  }
}
