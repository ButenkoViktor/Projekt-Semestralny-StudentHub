using StudentHub.Core.Entities.Identity;

namespace StudentHub.Core.Entities.Groups
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<GroupStudent> GroupStudents { get; set; } = new List<GroupStudent>();
        public ICollection<CourseGroup> CourseGroups { get; set; } = new List<CourseGroup>();
    }
}
