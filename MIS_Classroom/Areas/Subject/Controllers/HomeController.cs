using Microsoft.AspNetCore.Mvc;
using MIS_Classroom.Areas.Admin.Models;
using MIS_Classroom.Models;
using System.Linq;

namespace MIS_Classroom.Areas.Subject.Controllers
{
    [Area("Subject")]
    public class HomeController : Controller
    {
        private readonly tattsContext _context;
        public HomeController(tattsContext context) 
        {
            _context = context; 
        }
       
        public IActionResult Index()
        {
            return View();
        }
    }
}
