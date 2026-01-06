using System.Text.Json.Serialization;
using StudentHub.Core.Entities.Identity;

namespace StudentHub.Core.Entities.Groups
{
    public class TeacherGroup
    {
        public int GroupId { get; set; }

        [JsonIgnore]
        public Group Group { get; set; } = null!;

        public string TeacherId { get; set; } = null!;
        public ApplicationUser Teacher { get; set; } = null!;
    }
}