using Microsoft.AspNetCore.Identity;
using StudentHub.Core.Entities.Chat;
using StudentHub.Core.Entities.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Core.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the first name of the person.
        /// </summary>
        public string FirstName { get; set; } = default!;
        /// <summary>
        /// Gets or sets the last name of the person.
        /// </summary>
        public string LastName { get; set; } = default!;
        /// <summary>
        /// Gets or sets the URL of the user's avatar image.
        /// </summary>
        public string? AvatarUrl { get; set; }

        /// <summary>
        /// Gets or sets the group identifier associated with this person. Foreign key referencing the Group entity.
        /// </summary>
        public int? GroupId { get; set; }
        /// <summary>
        /// Gets or sets the group associated with this instance.
        /// </summary>
        public Group? Group { get; set; }

        /// <summary>
        /// Gets or sets the collection of chat messages associated with the chat session.
        /// </summary>
        public ICollection<ChatMessage>? Messages { get; set; }
        /// <summary>
        /// Gets or sets the collection of participants in the chat.
        /// </summary>
        public ICollection<ChatParticipant>? ChatParticipants { get; set; }
    }
}