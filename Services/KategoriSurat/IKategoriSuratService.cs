using System.Collections.Generic;
using System.Threading.Tasks;
using AspnetCoreMvcFull.DTOs;
using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.Services.KategoriSurat
{
  public interface IKategoriSuratService
  {
    Task<IEnumerable<Models.KategoriSurat>> GetAllAsync(string searchQuery);
    Task<Models.KategoriSurat> GetByIdAsync(int id);
    Task CreateAsync(KategoriSuratDto dto);
    Task UpdateAsync(KategoriSuratDto dto);
    Task DeleteAsync(int id);
  }
}
