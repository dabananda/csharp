﻿namespace StudentInfo.Models.ViewModel
{
    public class CreateStudentRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Department { get; set; }
        public string? Session { get; set; }
    }
}