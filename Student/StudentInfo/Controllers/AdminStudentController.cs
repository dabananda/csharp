using Microsoft.AspNetCore.Mvc;
using StudentInfo.Data;
using StudentInfo.Models.Domain;
using StudentInfo.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetStudents()
        {
            var studentList = await studentDbContext.StudentInfos.ToListAsync();
            return View(studentList);
        }

        // Get the add a new student form
        [HttpGet]
        public IActionResult CreateStudent()
        {
            return View();
        }

        // Create new student
        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentRequest createStudentRequest)
        {
            var student = new Student
            {
                Name = createStudentRequest.Name,
                Department = createStudentRequest.Department,
                Session = createStudentRequest.Session,
            };
            await studentDbContext.StudentInfos.AddAsync(student);
            await studentDbContext.SaveChangesAsync();
            return RedirectToAction("GetStudents");
        }

        // Get the form to edit student with id
        [HttpGet]
        public async Task<IActionResult> EditStudent(int id)
        {
            var student = await studentDbContext.StudentInfos.SingleOrDefaultAsync(x => x.Id == id);
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
        public async Task<IActionResult> EditStudent(EditStudentRequest editStudentRequest)
        {
            var student = new Student()
            {
                Id = editStudentRequest.Id,
                Name = editStudentRequest.Name,
                Department = editStudentRequest.Department,
                Session = editStudentRequest.Session,
            };
            var existingStudent = await studentDbContext.StudentInfos.FindAsync(student.Id);
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Department = student.Department;
                existingStudent.Session = student.Session;
                await studentDbContext.SaveChangesAsync();

            }
            return RedirectToAction("GetStudents");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStudent(EditStudentRequest editStudentRequest)
        {
            var student = await studentDbContext.StudentInfos.FindAsync(editStudentRequest.Id);
            if (student != null)
            {
                studentDbContext.StudentInfos.Remove(student);
                await studentDbContext.SaveChangesAsync();
            }
            return RedirectToAction("GetStudents");
        }
    }
}
