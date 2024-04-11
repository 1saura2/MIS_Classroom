using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MIS_Classroom.Areas.Admin.Models; 
using MIS_Classroom.Models; 

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
            var teachers = _context.TechengineeMisTeachers.Include(t => t.Subject).ToList();
            var students = _context.TechengineeMisStudents.ToList();
            var subjects = _context.TechengineeMisSubjects.ToList();
            var questions = _context.TechengineeMisQuestions.Include(q => q.Subject).ToList();


            var viewModel = new AdminDashboardViewModel
            {
                TechengineeMisTeachers = teachers,
                TechengineeMisStudents = students,
                TechengineeMisSubjects = subjects,
                TechengineeMisQuestions = questions
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddStudent(string name, string email, int semester)
        {
          
            var student = new TechengineeMisStudent
            {
                Name = name,
                Email = email,
                Semester = semester
            };

        
            _context.TechengineeMisStudents.Add(student);
            _context.SaveChanges();

            return Ok(); 
        }

        [HttpPost]
        public IActionResult AddTeacher(string name, string email, int subjectCode)
        {
           
            var teacher = new TechengineeMisTeacher
            {
                Name = name,
                Email = email,
                SubjectCode = subjectCode
            };

          
            _context.TechengineeMisTeachers.Add(teacher);
            _context.SaveChanges();

            return Ok();
        }
    }
}
