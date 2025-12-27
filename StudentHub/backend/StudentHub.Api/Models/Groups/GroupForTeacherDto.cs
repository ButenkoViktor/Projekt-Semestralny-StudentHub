namespace StudentHub.Api.Models.Groups
{
    public class GroupForTeacherDto
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; } = default!;
        public string CourseTitle { get; set; } = default!;
    }
}
