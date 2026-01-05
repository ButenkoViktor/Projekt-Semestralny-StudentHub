using StudentHub.Core.Entities.Identity;
using StudentHub.Core.Entities.Schedule;
using StudentHub.Core.Entities.Tasks;

namespace StudentHub.Core.Entities.Groups
{
    public class Course
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public string TeacherId { get; set; } = null!;
        public ApplicationUser Teacher { get; set; } = null!;

        public ICollection<CourseGroup> CourseGroups { get; set; } = new List<CourseGroup>();

        public ICollection<ScheduleItem> ScheduleItems { get; set; } = new List<ScheduleItem>();
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
