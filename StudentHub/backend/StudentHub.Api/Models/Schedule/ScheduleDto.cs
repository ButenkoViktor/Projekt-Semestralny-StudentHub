namespace StudentHub.Api.Models.Schedule
{
    public class ScheduleDto
    {
        public int Id { get; set; }
        public string CourseTitle { get; set; } = default!;
        public string TeacherName { get; set; } = default!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? LessonType { get; set; }
        public int? GroupId { get; set; }
    }
}