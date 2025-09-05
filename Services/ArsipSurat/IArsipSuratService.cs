using System.Collections.Generic;
using System.Threading.Tasks;
using AspnetCoreMvcFull.DTOs;

namespace AspnetCoreMvcFull.Services.ArsipSurat
{
  public interface IArsipSuratService
  {
    Task<IEnumerable<Models.ArsipSurat>> GetAllAsync(string searchQuery);
    Task<Models.ArsipSurat> GetByIdAsync(int id);
    Task CreateAsync(ArsipSuratDto dto);
    Task DeleteAsync(int id);
  }
}
