using Microsoft.AspNetCore.Authorization;
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
            var teacher = _context.TechengineeMisTeachers.FirstOrDefault(t => t.Email == email);

            if (teacher != null)
            {
                var subjectCode = teacher.SubjectCode;
                var questions = _context.TechengineeMisQuestions
                    .Where(q => q.SubjectCode == subjectCode)
                    .OrderBy(q => q.SubjectCode)
                    .ThenBy(q => q.Position)
                    .ToList();

                _context.SaveChanges();

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
            var question = _context.TechengineeMisQuestions.Include(q=>q.Subject).FirstOrDefault(q => q.QuestionId == id);
   
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

        public IActionResult ViewAnswers(int id)
        {
            var question = _context.TechengineeMisQuestions.Include(q=>q.Subject).FirstOrDefault(q => q.QuestionId == id);
            var answers = _context.TechengineeMisAnswers
                        .Include(a => a.Student)
                        .Where(a => a.QuestionId == id)
                        .ToList();
            ViewBag.Question = question;

            return View(answers);
        }

        [HttpPost]
        public IActionResult UpdatePositions(List<int> questionIds, int subjectCode)
        {
            var questions = _context.TechengineeMisQuestions
                .Where(q => q.SubjectCode == subjectCode)
                .OrderBy(q => q.Position)
                .ToList();

            for (int i = 0; i < questionIds.Count; i++)
            {
                var questionId = questionIds[i];
                var question = questions.FirstOrDefault(q => q.QuestionId == questionId);
                if (question != null)
                {
                    question.Position = i + 1;
                }
            }

            _context.SaveChanges();

            return Ok();
           /* return RedirectToAction("ListQuestions");*/
        }



    }
}