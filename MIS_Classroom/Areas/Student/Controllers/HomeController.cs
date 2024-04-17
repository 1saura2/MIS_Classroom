using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MIS_Classroom.Areas.Student.Models;
using MIS_Classroom.Models;
using System.Linq;

namespace MIS_Classroom.Areas.Student.Controllers
{

    [Area("Student")]
    public class HomeController : Controller
    {
        private readonly tattsContext _context;

        public HomeController(tattsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var viewModel = new ViewQuestionsViewModel
            {
                Subjects = _context.TechengineeMisSubjects.ToList()
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult FetchQuestions(int subjectCode)
        {
            var questions = _context.TechengineeMisQuestions
                .Where(q => q.SubjectCode == subjectCode)
                .ToList();

            return PartialView("QuestionsPartial", questions);
        }
    }
}
