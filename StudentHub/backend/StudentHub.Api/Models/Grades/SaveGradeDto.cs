namespace StudentHub.Api.Models.Grades
{
    public class SaveGradeDto
    {
        public string StudentId { get; set; } = default!;
        public int CourseId { get; set; }
        public int GroupId { get; set; }
        public DateTime Date { get; set; }
        public int? Grade { get; set; }
        public bool IsPresent { get; set; }
    }
}
