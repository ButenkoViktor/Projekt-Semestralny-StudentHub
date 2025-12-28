using Microsoft.AspNetCore.Mvc;

namespace StudentHub.Api.Models.Courses
{
    public class CreateCourseDto
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public string TeacherId { get; set; }
    }
}
