using StudentInfo.Models.Domain;

namespace StudentInfo.Repository
{
    public class StudentRepo : IStudentRepo
    {
        public Task<Student> AddStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public Task<Student?> DeleteStudent(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Student>> GetAllStudents()
        {
            throw new NotImplementedException();
        }

        public Task<Student?> GetStudentById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Student?> UpdateStudent(int id)
        {
            throw new NotImplementedException();
        }
    }
}
