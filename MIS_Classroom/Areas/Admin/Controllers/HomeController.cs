using Microsoft.AspNetCore.Authorization;
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

        // For Teacher

        public IActionResult ListTeacher()
        {
            var teachers = _context.TechengineeMisTeachers.Include(t => t.Subject).ToList();
            return View(teachers);
        }

        public IActionResult AddTeacher()
        {
            var subjects = _context.TechengineeMisSubjects.ToList();
            return View(subjects);
        }

        [HttpPost]
        public IActionResult AddTeacher(TechengineeMisTeacher teacher, string password)
        {
            _context.TechengineeMisTeachers.Add(teacher);
            _context.SaveChanges();

            var teacherId = teacher.TeacherId;

            var userType = _context.TechengineeMisUserTypes.FirstOrDefault(u => u.UserType.ToLower() == "teacher")?.TypeId;

            var credential = new TechengineeMisCredential
            {
                Email = teacher.Email,
                Password = password,
                UserType = userType
            };

            _context.TechengineeMisCredentials.Add(credential);
            _context.SaveChanges();

            return RedirectToAction(nameof(ListTeacher));
        }

        public IActionResult EditTeacher(int? id)
        {
            var teacher = _context.TechengineeMisTeachers
                .Include(t => t.Subject)
                .FirstOrDefault(t => t.TeacherId == id);

            var subjects = _context.TechengineeMisSubjects.ToList();

            ViewBag.Subjects = subjects;
            return View(teacher);
        }

        [HttpPost]
        public IActionResult EditTeacher(int id, TechengineeMisTeacher teacher, string password)
        {
            teacher.TeacherId = id;

            _context.Update(teacher);
            _context.SaveChanges();

            if (!string.IsNullOrEmpty(password))
            {
                var credential = _context.TechengineeMisCredentials.FirstOrDefault(c => c.Email == teacher.Email);
                if (credential != null)
                {
                    credential.Password = password;
                    _context.Update(credential);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction(nameof(ListTeacher));
        }

        public IActionResult DeleteTeacher(int? id)
        {
            var teacher = _context.TechengineeMisTeachers.FirstOrDefault(t => t.TeacherId == id);
            return View(teacher);
        }

        [HttpPost, ActionName("DeleteTeacher")]
        public IActionResult DeleteTeacherConfirm(int id)
        {
            var teacher = _context.TechengineeMisTeachers.FirstOrDefault(t => t.TeacherId == id);
            var credential = _context.TechengineeMisCredentials.FirstOrDefault(c => c.Email == teacher.Email);

            _context.TechengineeMisCredentials.Remove(credential);
            _context.TechengineeMisTeachers.Remove(teacher);

            _context.SaveChanges();

            return RedirectToAction(nameof(ListTeacher));
        }

        // For Student

        public IActionResult ListStudent()
        {
            var students = _context.TechengineeMisStudents.ToList();
            return View(students);
        }

        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(TechengineeMisStudent student, string password)
        {
            _context.TechengineeMisStudents.Add(student);
            _context.SaveChanges();

            var studentId = student.StudentId;

            var userType = _context.TechengineeMisUserTypes.FirstOrDefault(u => u.UserType.ToLower() == "student")?.TypeId;

            var credential = new TechengineeMisCredential
            {
                Email = student.Email,
                Password = password,
                UserType = userType
            };

            _context.TechengineeMisCredentials.Add(credential);
            _context.SaveChanges();

            return RedirectToAction(nameof(ListStudent));
        }

        public IActionResult EditStudent(int? id)
        {
            var student = _context.TechengineeMisStudents.FirstOrDefault(s => s.StudentId == id);
            return View(student);
        }

        [HttpPost]
        public IActionResult EditStudent(int id, TechengineeMisStudent student, string password)
        {
            student.StudentId = id;


            _context.Update(student);
            _context.SaveChanges();

            if (!string.IsNullOrEmpty(password))
            {
                var credential = _context.TechengineeMisCredentials.FirstOrDefault(c => c.Email == student.Email);
                if (credential != null)
                {
                    credential.Password = password;
                    _context.Update(credential);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction(nameof(ListStudent));
        }

        public IActionResult DeleteStudent(int? id)
        {
            var student = _context.TechengineeMisStudents.FirstOrDefault(s => s.StudentId == id);
            return View(student);
        }

        [HttpPost, ActionName("DeleteStudent")]
        public IActionResult DeleteStudentConfirm(int id)
        {
            var student = _context.TechengineeMisStudents.FirstOrDefault(s => s.StudentId == id);
            var credential = _context.TechengineeMisCredentials.FirstOrDefault(c => c.Email == student.Email);

            _context.TechengineeMisCredentials.Remove(credential);
            _context.TechengineeMisStudents.Remove(student);

            _context.SaveChanges();

            return RedirectToAction(nameof(ListStudent));
        }





        public IActionResult Index()
        {
        
            return View();
        }
    }
}
