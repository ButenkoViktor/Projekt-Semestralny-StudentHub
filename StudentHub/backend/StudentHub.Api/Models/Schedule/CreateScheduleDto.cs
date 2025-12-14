namespace StudentHub.Api.Models.Schedule
{
    public class CreateScheduleDto
    {
        public int CourseId { get; set; }
        public string TeacherName { get; set; } = default!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? GroupId { get; set; }
        public string? LessonType { get; set; }
    }
}