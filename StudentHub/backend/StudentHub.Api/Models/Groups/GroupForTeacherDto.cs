namespace StudentHub.Api.Models.Groups
{
    public class GroupForTeacherDto
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; } = default!;
        public int CourseId { get; set; }        
        public string CourseTitle { get; set; } = default!;
    }
}
