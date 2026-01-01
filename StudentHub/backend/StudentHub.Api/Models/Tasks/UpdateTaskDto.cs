namespace StudentHub.Api.Models.Tasks
{
    public class UpdateTaskDto
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime Deadline { get; set; }
    }
}
