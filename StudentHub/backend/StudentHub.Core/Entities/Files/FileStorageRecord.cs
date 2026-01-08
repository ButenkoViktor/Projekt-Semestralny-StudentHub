using System;
using StudentHub.Core.Entities.Identity;

namespace StudentHub.Core.Entities.Files
{
    public class FileStorageRecord
    {
        public int Id { get; set; }

        public string FilePath { get; set; } = default!;

        public string FileName { get; set; } = default!;

        public string? UploadedById { get; set; }

        public ApplicationUser? UploadedBy { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}