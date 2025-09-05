using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcFull.Controllers
{
  public class AboutController : Controller
  {
    public IActionResult Index()
    {
      // Controller ini hanya menampilkan halaman statis About
      return View();
    }
  }
}
