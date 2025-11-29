using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentHub.Core.Entities.Groups;
using StudentHub.Core.Entities.Chat;

namespace StudentHub.Core.Entities.Users
{
    public class User
    {
        /// <summary>
        /// Primary key of the user.
        /// </summary>
        public string Id { get; set; } = default!;

        /// <summary>
        /// Gets or sets the first name of the person.
        /// </summary>
        public string FirstName { get; set; } = default!;

        /// <summary>
        /// Gets or sets the last name of the person.
        /// </summary>
        public string LastName { get; set; } = default!;

        /// <summary>
        /// Gets or sets the email address associated with the user.
        /// </summary>
        public string Email { get; set; } = default!;

        /// <summary>
        /// Gets or sets the URL of the user's avatar image.
        /// </summary>
        public string? AvatarUrl { get; set;}

        /// <summary>
        /// Gets or sets the role associated with the current user or entity.
        /// </summary>
        public string Role { get; set; } = "Student";

        /// <summary>
        /// Gets or sets the identifier of the group associated with this entity. Foreign key referencing the Group entity.
        /// </summary>
        public int? GroupId { get; set; }

        /// <summary>
        /// Gets or sets the group associated with this instance.
        /// </summary>
        public Group? Group { get; set; }

        /// <summary>
        /// Gets or sets the collection of chat messages associated with the conversation.
        /// </summary>
        /// <remarks>The collection may be null if no messages have been loaded or assigned. Modifying the
        /// collection does not automatically persist changes; ensure that updates are saved as appropriate for the
        /// application's data model.</remarks>
        public ICollection<Chat.ChatMessage>? Messages { get; set; }

        /// <summary>
        /// Gets or sets the collection of participants in the chat.
        /// </summary>
        /// <remarks>The collection may be null if no participants have been added. Modifying the
        /// collection does not automatically update the chat state; ensure changes are synchronized as needed in
        /// multi-threaded scenarios.</remarks>
        public ICollection<Chat.ChatParticipant>? ChatParticipants { get; set; }
    }
}


