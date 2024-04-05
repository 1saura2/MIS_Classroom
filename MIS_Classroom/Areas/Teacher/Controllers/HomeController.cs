using Microsoft.AspNetCore.Mvc;

namespace MIS_Classroom.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
