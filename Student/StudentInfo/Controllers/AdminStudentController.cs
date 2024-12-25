using Microsoft.AspNetCore.Mvc;
using StudentInfo.Models.Domain;
using StudentInfo.Models.ViewModel;
using StudentInfo.Repository;

namespace StudentInfo.Controllers
{
    public class AdminStudentController : Controller
    {
        private readonly IStudentRepo studentRepo;

        public AdminStudentController(IStudentRepo studentRepo) 
        {
            this.studentRepo = studentRepo;
        }

        // Get the student list
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var studentList = await studentRepo.GetAllStudents();
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
            await studentRepo.AddStudent(student);
            return RedirectToAction("GetStudents");
        }

        // Get the form to edit student with id
        [HttpGet]
        public async Task<IActionResult> EditStudent(int id)
        {
            var student = await studentRepo.GetStudentById(id);
            if (student != null)
            {
                var editStudentRequest = new EditStudentRequest
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
            var updatedStudent = await studentRepo.UpdateStudent(student);
            if (updatedStudent != null)
            {
                return RedirectToAction("GetStudents");
            }
            return RedirectToAction("GetStudents");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStudent(EditStudentRequest editStudentRequest)
        {
            var student = await studentRepo.DeleteStudent(editStudentRequest.Id);
            if (student != null)
            {
                return RedirectToAction("GetStudents");
            }
            return RedirectToAction("GetStudents");
        }
    }
}
