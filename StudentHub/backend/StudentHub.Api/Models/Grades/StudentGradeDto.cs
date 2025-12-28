namespace StudentHub.Api.Models.Grades
{
    public class StudentGradeDto
    {
        public string StudentId { get; set; } = default!;
        public string StudentName { get; set; } = default!;
        public int? Grade { get; set; }
        public bool IsPresent { get; set; }
        public DateTime Date { get; set; }
    }
}
