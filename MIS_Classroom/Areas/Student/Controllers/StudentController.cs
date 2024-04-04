using Microsoft.AspNetCore.Mvc;

namespace MIS_Classroom.Areas.Student.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
