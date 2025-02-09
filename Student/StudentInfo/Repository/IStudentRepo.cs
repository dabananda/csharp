﻿using StudentInfo.Models.Domain;

namespace StudentInfo.Repository
{
    public interface IStudentRepo
    {
        Task<IEnumerable<Student>> GetAllStudents();
        Task<Student?> GetStudentById(int id);
        Task<Student> AddStudent(Student student);
        Task<Student?> UpdateStudent(Student student);
        Task<Student?> DeleteStudent(int id);
    }
}
