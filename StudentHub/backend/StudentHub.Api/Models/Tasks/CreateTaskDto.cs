namespace StudentHub.Api.Models.Tasks
{
    public class CreateTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public int? CourseId { get; set; }
        public int? GroupId { get; set; }
    }
}
