using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentHub.Core.Entities.Identity;

namespace StudentHub.Core.Entities.Announcements
{
    public class Announcement
    {
        public int Id { get; set; }

        public string Title { get; set; } = default!;

        public string Content { get; set; } = default!;

        public bool Published { get; set; } = true;

        public int? GroupId { get; set; }

        public string? TargetGroup { get; set; }

        public string AuthorId { get; set; } = default!;

        public ApplicationUser Author { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}