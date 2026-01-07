namespace StudentHub.Api.Models.Grades
{
    public class ClearGradesDto
    {
        public int GroupId { get; set; }
        public int CourseId { get; set; }
        public DateTime? Date { get; set; } 
    }
}