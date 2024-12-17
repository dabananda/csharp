using CGPACalculatorApp.Data;
using CGPACalculatorApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CGPACalculatorApp.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AppDbContext _context;
        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // Get student list
        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _context.Students.ToList();
            return View(students);
        }

        // Get add new student form
        [HttpGet]
        public IActionResult PostStudent()
        {
            return View();
        }

        // Post new student
        [HttpPost]
        public IActionResult PostStudent(PostStudentReq postStudentReq)
        {
            var student = new Student()
            {
                Name = postStudentReq.Name,
                Department = postStudentReq.Department,
                Session = postStudentReq.Session,
                Semester = postStudentReq.Semester,
                GPA = postStudentReq. GPA,
            };
            _context.Students.Add(student);
            _context.SaveChanges();
            return RedirectToAction("GetStudents");
        }
    }
}
