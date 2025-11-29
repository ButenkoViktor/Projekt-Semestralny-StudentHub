using System;
namespace StudentHub.Core.Entities.Files
{
    public class FileStorageRecord
    {
        /// <summary>
        /// Primary key of the file storage record.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the file system path associated with the current instance.
        /// </summary>
        public string FilePath { get; set; } = default!;
        /// <summary>
        /// Gets or sets the name of the file associated with this instance.
        /// </summary>
        public string FileName { get; set; } = default!;
        /// <summary>
        /// Gets or sets the name or identifier of the user who uploaded the item.
        /// </summary>
        public string? UploadedBy { get; set; }
        /// <summary>
        /// Gets or sets the date and time, in UTC, when the item was uploaded.
        /// </summary>
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}