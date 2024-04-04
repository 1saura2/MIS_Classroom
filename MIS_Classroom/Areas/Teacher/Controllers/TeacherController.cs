using Microsoft.AspNetCore.Mvc;

namespace MIS_Classroom.Areas.Teacher.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
