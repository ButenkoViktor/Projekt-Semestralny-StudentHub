using Microsoft.AspNetCore.Identity;
using StudentHub.Core.Entities.Announcements;
using StudentHub.Core.Entities.Groups;

namespace StudentHub.Core.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AvatarUrl { get; set; }

        public ICollection<Course> TeachingCourses { get; set; } = new List<Course>();

        public ICollection<GroupStudent> GroupStudents { get; set; } = new List<GroupStudent>();

        public ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();
    }
}
