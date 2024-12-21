using Microsoft.AspNetCore.Mvc;
using StudentInfo.Data;
using StudentInfo.Models.Domain;
using StudentInfo.Models.ViewModel;

namespace StudentInfo.Controllers
{
    public class AdminStudentController : Controller
    {
        private readonly StudentDbContext studentDbContext;

        public AdminStudentController(StudentDbContext studentDbContext) 
        {
            this.studentDbContext = studentDbContext;
        }

        // Get the student list
        [HttpGet]
        public IActionResult GetStudents()
        {
            var studentList = studentDbContext.StudentInfos.ToList();
            return View(studentList);
        }

        // Get the add a new student form
        [HttpGet]
        public IActionResult PostStudent()
        {
            return View();
        }

        // Create new student
        [HttpPost]
        public IActionResult PostStudent(PostStudentRequest postStudentRequest)
        {
            var student = new Student
            {
                Name = postStudentRequest.Name,
                Department = postStudentRequest.Department,
                Session = postStudentRequest.Session,
            };
            studentDbContext.StudentInfos.Add(student);
            studentDbContext.SaveChanges();
            return RedirectToAction("GetStudents");
        }

        // Get the form to edit student with id
        [HttpGet]
        public IActionResult EditStudent(int id)
        {
            var student = studentDbContext.StudentInfos.SingleOrDefault(x => x.Id == id);
            if (student != null)
            {
                var editStudentRequest = new EditStudentRequest()
                {
                    Id = student.Id,
                    Name = student.Name,
                    Department = student.Department,
                    Session = student.Session,
                };
                return View(editStudentRequest);
            }
            return View(null);
        }

        // Post the updated student
        [HttpPost]
        public IActionResult EditStudent(EditStudentRequest editStudentRequest)
        {
            var student = new Student()
            {
                Id = editStudentRequest.Id,
                Name = editStudentRequest.Name,
                Department = editStudentRequest.Department,
                Session = editStudentRequest.Session,
            };
            var existingStudent = studentDbContext.StudentInfos.Find(student.Id);
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Department = student.Department;
                existingStudent.Session = student.Session;
                studentDbContext.SaveChanges();

            }
            return RedirectToAction("GetStudents");
        }

        [HttpPost]
        public IActionResult DeleteStudent(EditStudentRequest editStudentRequest)
        {
            var student = studentDbContext.StudentInfos.Find(editStudentRequest.Id);
            if (student != null)
            {
                studentDbContext.StudentInfos.Remove(student);
                studentDbContext.SaveChanges();
            }
            return RedirectToAction("GetStudents");
        }
    }
}
