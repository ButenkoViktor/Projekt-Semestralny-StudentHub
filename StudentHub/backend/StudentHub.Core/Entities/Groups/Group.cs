using StudentHub.Core.Entities.Groups;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<GroupStudent> GroupStudents { get; set; } = new List<GroupStudent>();
    public ICollection<TeacherGroup> TeacherGroups { get; set; } = new List<TeacherGroup>();
}