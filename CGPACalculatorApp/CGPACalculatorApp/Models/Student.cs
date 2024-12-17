using System.ComponentModel.DataAnnotations;

namespace CGPACalculatorApp.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Session { get; set; }

        [Required]
        public string Semester { get; set; }

        [Required]
        [Range(0.0, 4.0)]
        public double GPA { get; set; }

        public double CGPA { get; set; }
    }
}
