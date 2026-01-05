namespace StudentHub.Core.Entities.Groups
{
    public class CourseGroup
    {
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;
    }
}
