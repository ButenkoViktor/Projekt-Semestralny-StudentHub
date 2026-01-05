using StudentHub.Core.Entities.Identity;

namespace StudentHub.Core.Entities.Groups
{
    public class GroupStudent
    {
        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;

        public string StudentId { get; set; } = null!;
        public ApplicationUser Student { get; set; } = null!;
    }
}
