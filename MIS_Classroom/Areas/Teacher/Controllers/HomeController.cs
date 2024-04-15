/*using Microsoft.AspNetCore.Mvc;
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
            return View(viewModel); 
        }

        [HttpPost]
        public IActionResult Index(int subjectId, string questionText) 
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
            return RedirectToAction(nameof(Index)); 
        }
    }
}
*/
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

        public IActionResult Index(int subjectId)
        {
            var viewModel = new AddQuestionViewModel
            {
                Subjects = _context.TechengineeMisSubjects.ToList(),
                SubjectId = subjectId,
                Questions = _context.TechengineeMisQuestions
                    .Where(q => q.SubjectCode == subjectId)
                    .ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(int subjectId, string questionText)
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
            return RedirectToAction(nameof(Index), new { subjectId });
        }

        [HttpPost]
        public IActionResult Update(int id, string updatedQuestionText, int subjectId)
        {
            var question = _context.TechengineeMisQuestions.Find(id);
            if (question != null)
            {
                question.QuestionsTxt = updatedQuestionText;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index), new { subjectId });
        }

        [HttpPost]
        public IActionResult Delete(int id, int subjectId)
        {
            var question = _context.TechengineeMisQuestions.Find(id);
            if (question != null)
            {
                _context.TechengineeMisQuestions.Remove(question);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index), new { subjectId });
        }
    }
}

