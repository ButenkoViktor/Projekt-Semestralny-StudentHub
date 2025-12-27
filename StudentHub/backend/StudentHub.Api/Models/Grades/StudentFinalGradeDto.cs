namespace StudentHub.Api.Models.Grades
{
    public class StudentFinalGradeDto
    {
        public string StudentId { get; set; } = default!;
        public string StudentName { get; set; } = default!;
        public double FinalGrade { get; set; }
        public int TotalLessons { get; set; }
        public int AttendedLessons { get; set; }
    }
}
