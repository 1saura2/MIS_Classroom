using Microsoft.AspNetCore.Mvc;
using MIS_Classroom.Areas.Admin.Models;
using MIS_Classroom.Models;
using System.Linq;

namespace MIS_Classroom.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly tattsContext _context;

        public HomeController(tattsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var teachers = _context.TechengineeMisTeachers.ToList();
            var students = _context.TechengineeMisStudents.ToList();
            var viewModel = new AdminDashboardViewModel
            {
                TechengineeMisTeachers = teachers,
                TechengineeMisStudents = students
            };
            return View(viewModel);
        }
    }
}
