using System;

namespace StudentHub.Core.Entities.Notes
{
    public class Note
    {
        public int Id { get; set; }

        public string UserId { get; set; } = default!;

        public string Title { get; set; } = default!;

        public string Content { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
