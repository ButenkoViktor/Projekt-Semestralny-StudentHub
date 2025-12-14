namespace StudentHub.Api.Models.Courses
{
    public class CourseDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
    }
}