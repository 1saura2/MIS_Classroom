using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            return View();
        }

        public IActionResult ListQuestions()
        {
            var email = HttpContext.Session.GetString("Email");

            var teacher = _context.TechengineeMisTeachers
                            .FirstOrDefault(t => t.Email == email);

            if (teacher != null)
            {
                var subjectCode = teacher.SubjectCode;

                var questions = _context.TechengineeMisQuestions
                                    .Where(q => q.SubjectCode == subjectCode)
                                    .ToList();

                return View(questions);
            }
            else
            {
                return RedirectToAction("Login"); 
            }
        }

        public IActionResult AddQuestion()
        {
            var teacherEmail = HttpContext.Session.GetString("Email");

            var teacher = _context.TechengineeMisTeachers
                .Include(t => t.Subject)
                .FirstOrDefault(t => t.Email == teacherEmail);

            if (teacher == null)
            {
             
                return RedirectToAction("Index", "Home"); 
            }

    
            return View(teacher);
        }

        [HttpPost]
        public IActionResult AddQuestion(TechengineeMisQuestion question)
        {
            _context.TechengineeMisQuestions.Add(question);
            _context.SaveChanges();

            return RedirectToAction("AddQuestion", "Home"); 
        }

        public IActionResult EditQuestion(int id)
        {
            var question = _context.TechengineeMisQuestions.FirstOrDefault(q => q.QuestionId == id);
   
            return View(question);
        }

        [HttpPost]
        public IActionResult EditQuestion(int questionId, string questionText)
        {
            var question = _context.TechengineeMisQuestions.FirstOrDefault(q => q.QuestionId == questionId);
 
            question.QuestionsTxt = questionText;
            _context.SaveChanges();

            return RedirectToAction("ListQuestions", "Home"); 
        }

        public IActionResult DeleteQuestion(int id)
        {
            var question = _context.TechengineeMisQuestions.FirstOrDefault(q => q.QuestionId == id);

            return View(question);
        }

        [HttpPost, ActionName("DeleteQuestion")]
        public IActionResult DeleteConfirmed(int id)
        {
            var question = _context.TechengineeMisQuestions.FirstOrDefault(q => q.QuestionId == id);

            _context.TechengineeMisQuestions.Remove(question);
            _context.SaveChanges();

            return RedirectToAction("ListQuestions", "Home");
        }


    }
}





















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



/*using Microsoft.AspNetCore.Authorization;
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
*/
