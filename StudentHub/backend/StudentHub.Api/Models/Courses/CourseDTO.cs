namespace StudentHub.Api.Models.Courses
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string TeacherId { get; set; }
        public List<GroupShortDto> Groups { get; set; } = new();
    }

    public class GroupShortDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}