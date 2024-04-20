using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MIS_Classroom.Models;
using System.Linq;
using MIS_Classroom.Areas.Student.Models;

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

        private int GetStudentIdFromSession()
        {
            var email = HttpContext.Session.GetString("Email");
           
            var student = _context.TechengineeMisStudents.FirstOrDefault(s => s.Email == email);
            return student.StudentId; 
        }



        public IActionResult Index()
        {
            var subjects = _context.TechengineeMisSubjects.ToList();
            return View(subjects);
        }





        public IActionResult FetchQuestions(int subjectCode)
        {
            var questions = _context.TechengineeMisQuestions
                                .Where(q => q.SubjectCode == subjectCode)
                                .OrderBy(q => q.Position)
                                .ToList();

            return View(questions);
        }





        public IActionResult AnswerView(int questionId)
        {
            int studentId = GetStudentIdFromSession();

            var answer = _context.TechengineeMisAnswers
                .FirstOrDefault(a => a.StudentId == studentId && a.QuestionId == questionId);

            var question = _context.TechengineeMisQuestions
                .FirstOrDefault(q => q.QuestionId == questionId);

            if (answer != null)
            {
                ViewBag.Answer = answer;
                return View("AlreadyAnsweredView", question);
            }
            else
            {
                return View("ProvideAnswerView", question);
            }
        }





        [HttpPost]
        public IActionResult SubmitAnswer(TechengineeMisAnswer answer)
        {

            int studentId = GetStudentIdFromSession();

            answer.StudentId = studentId;

            _context.TechengineeMisAnswers.Add(answer);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }





    }
}
