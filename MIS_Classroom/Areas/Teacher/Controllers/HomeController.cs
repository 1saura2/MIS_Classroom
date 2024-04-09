using Microsoft.AspNetCore.Mvc;
using MIS_Classroom.Areas.Teacher.Models;
using MIS_Classroom.Models;
using System.Linq;

namespace MIS_Classroom.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    public class HomeController : Controller
    {
        private readonly tattsContext _context;

        public HomeController(tattsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var viewModel = new AddQuestionViewModel
            {
                Subjects = _context.TechengineeMisSubjects.ToList()
            };
            return View(viewModel); // Pass the viewModel to the view
        }

        [HttpPost]
        public IActionResult Index(string subjectId, string questionText)
        {
            if (!string.IsNullOrEmpty(questionText))
            {
                var question = new TechengineeMisQuestion
                {
                    SubjectCode = subjectId,
                    QuestionsTxt = questionText
                };
                _context.TechengineeMisQuestions.Add(question);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index)); // Redirect to the Index action of this controller
        }
    }
}
