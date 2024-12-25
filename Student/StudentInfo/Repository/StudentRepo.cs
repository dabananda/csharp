using Microsoft.EntityFrameworkCore;
using StudentInfo.Data;
using StudentInfo.Models.Domain;

namespace StudentInfo.Repository
{
    public class StudentRepo : IStudentRepo
    {
        private readonly StudentDbContext studentDbContext;

        public StudentRepo(StudentDbContext studentDbContext)
        {
            this.studentDbContext = studentDbContext;
        }
        public async Task<Student> AddStudent(Student student)
        {
            await studentDbContext.StudentInfos.AddAsync(student);
            await studentDbContext.SaveChangesAsync();
            return student;
        }

        public async Task<Student?> DeleteStudent(int id)
        {
            var student = await studentDbContext.StudentInfos.FindAsync(id);
            if (student != null)
            {
                studentDbContext.StudentInfos.Remove(student);
                await studentDbContext.SaveChangesAsync();
                return student;
            }
            return null;
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await studentDbContext.StudentInfos.ToListAsync();
        }

        public async Task<Student?> GetStudentById(int id)
        {
            return await studentDbContext.StudentInfos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Student?> UpdateStudent(Student student)
        {
            var existingStudent = await studentDbContext.StudentInfos.FindAsync(student.Id);
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Department = student.Department;
                existingStudent.Session = student.Session;
                await studentDbContext.SaveChangesAsync();
                return existingStudent;
            }
            return null;
        }
    }
}
