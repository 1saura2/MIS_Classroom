using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MIS_Classroom.Models;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace MIS_Classroom.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly tattsContext _context;

        public HomeController(ILogger<HomeController> logger, tattsContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Home()
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                return RedirectToDashboard();
            }
            return View("Index");
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                return RedirectToDashboard();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(TechengineeMisCredential user)
        {
            var myuser = _context.TechengineeMisCredentials.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
            if (myuser != null)
            {
                HttpContext.Session.SetString("Email", user.Email);
                var userType = _context.TechengineeMisUserTypes.FirstOrDefault(u => u.TypeId == myuser.UserType)?.UserType;
                if (userType != null)
                {
                    HttpContext.Session.SetString("UserType", userType);
                    return RedirectToDashboard();
                }
            }
            else
            {
                ViewBag.Message = "Login User Not Found!";
            }
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Registration()
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                return RedirectToDashboard();
            }

            var subjects = _context.TechengineeMisSubjects.ToList();
            return View(subjects);
        }

        [HttpPost]
        public IActionResult Registration(string userType, string email, string name, string password, int? semester, int? subjectCode)
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                return RedirectToDashboard();
            }

            var subjects = _context.TechengineeMisSubjects.ToList();
            if (ModelState.IsValid)
            {
                userType = userType.ToLower();

                if (_context.TechengineeMisCredentials.Any(c => c.Email == email))
                {
                    ViewBag.Message = "Email address is already in use!";
                    return View("Registration", subjects);
                }

                if (userType == "teacher")
                {
                    var teacher = new TechengineeMisTeacher
                    {
                        Email = email,
                        Name = name,
                        SubjectCode = subjectCode
                    };
                    _context.TechengineeMisTeachers.Add(teacher);

                    var credential = new TechengineeMisCredential
                    {
                        Email = email,
                        Password = password,
                        UserType = _context.TechengineeMisUserTypes.FirstOrDefault(u => u.UserType.ToLower() == "teacher")?.TypeId
                    };
                    _context.TechengineeMisCredentials.Add(credential);
                }
                else if (userType == "student")
                {
                    var student = new TechengineeMisStudent
                    {
                        Email = email,
                        Name = name,
                        Semester = semester
                    };
                    _context.TechengineeMisStudents.Add(student);

                    var credential = new TechengineeMisCredential
                    {
                        Email = email,
                        Password = password,
                        UserType = _context.TechengineeMisUserTypes.FirstOrDefault(u => u.UserType.ToLower() == "student")?.TypeId
                    };
                    _context.TechengineeMisCredentials.Add(credential);
                }

                _context.SaveChanges();

                return RedirectToAction("Login", "Home");
            }

            return View("Registration", subjects);
        }

        private IActionResult RedirectToDashboard()
        {
            var userType = HttpContext.Session.GetString("UserType");
            switch (userType)
            {
                case "Admin":
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                case "Student":
                    return RedirectToAction("Index", "Home", new { area = "Student" });
                case "Teacher":
                    return RedirectToAction("Index", "Home", new { area = "Teacher" });
                default:
                    return RedirectToAction("Index", "Home");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
