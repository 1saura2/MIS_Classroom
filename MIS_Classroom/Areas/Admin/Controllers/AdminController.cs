using Microsoft.AspNetCore.Mvc;

namespace MIS_Classroom.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
