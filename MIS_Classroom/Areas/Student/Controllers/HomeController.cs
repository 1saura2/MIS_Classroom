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

        private int? GetStudentIdFromSession()
        {
            var email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }

            var student = _context.TechengineeMisStudents.FirstOrDefault(s => s.Email == email);
            return student?.StudentId ?? default(int);
        }


        public IActionResult Index()
        {
            var subjects = _context.TechengineeMisSubjects.ToList();
            return View(subjects);
        }





        public IActionResult FetchQuestions(int subjectCode)
        {
            var studentId = GetStudentIdFromSession();
            var questions = _context.TechengineeMisQuestions
                .Where(q => q.SubjectCode == subjectCode)
                .ToList();
            return View(questions);
        }

        public IActionResult AnswerView(int questionId)
        {
            var studentId = GetStudentIdFromSession();



            var question = _context.TechengineeMisQuestions.FirstOrDefault(q => q.QuestionId == questionId);
;

            return View(question);
        }




        [HttpPost]
        public IActionResult SubmitAnswer(TechengineeMisAnswer answer)
        {

            var studentId = GetStudentIdFromSession();

            answer.StudentId = studentId.Value;

            _context.TechengineeMisAnswers.Add(answer);
            _context.SaveChanges();

            return RedirectToAction(nameof(FetchQuestions));
        }





    }
}
