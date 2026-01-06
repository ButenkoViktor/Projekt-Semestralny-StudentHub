using StudentHub.Core.Entities.Identity;
using System.Text.Json.Serialization;

namespace StudentHub.Core.Entities.Groups
{
    public class GroupStudent
    {
        public int GroupId { get; set; }

        [JsonIgnore]
        public Group Group { get; set; } = null!;

        public string StudentId { get; set; } = null!;
        public ApplicationUser Student { get; set; } = null!;
    }
}
