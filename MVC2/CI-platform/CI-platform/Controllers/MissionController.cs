using Microsoft.AspNetCore.Mvc;

namespace CI_platform.Controllers
{
    public class MissionController : Controller
    {
        public IActionResult MissionDetail()
        {
            return View();
        }
    }
}
