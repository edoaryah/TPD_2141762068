namespace AspnetCoreMvcFull.Services.Storage
{
  public interface IFileStorageService
  {
    Task<string> SaveFileAsync(IFormFile file, string subfolder);
    void DeleteFile(string relativePath);
  }
}
