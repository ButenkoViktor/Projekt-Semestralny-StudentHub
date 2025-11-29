using System;
using StudentHub.Core.Entities.Users;

namespace StudentHub.Core.Entities.Notifications
{
    public class Notification
    {
        /// <summary>
        /// Primary key of the notification.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the unique identifier for the user.Foreign key referencing the User entity.
        /// </summary>
        public string UserId { get; set; } = default!;
        /// <summary>
        /// Gets or sets the user associated with the current context.
        /// </summary>
        public User User { get; set; } = default!;
        /// <summary>
        /// Gets or sets the message text associated with this instance.
        /// </summary>
        public string Message { get; set; } = default!;
        /// <summary>
        /// Gets or sets a value indicating whether the item has been marked as read.
        /// </summary>
        public bool IsRead { get; set; } = false;
        /// <summary>
        /// Gets or sets the date and time when the object was created, in UTC.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}