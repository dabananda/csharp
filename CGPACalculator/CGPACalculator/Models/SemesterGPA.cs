namespace CGPACalculator.Models
{
    public class SemesterGPA
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Session {  get; set; }
        public int Semister { get; set; }
        public double GPA { get; set; }
    }
}
