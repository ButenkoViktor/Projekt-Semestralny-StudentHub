using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentHub.Core.Entities.Groups;


namespace StudentHub.Core.Entities.Groups
{
    public class Group
    {
        /// <summary>
        /// Primary key of the group.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name associated with the object.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Gets or sets the collection of students associated with this entity.
        /// </summary>
        public ICollection<StudentHub.Core.Entities.Users.User>? Students { get; set; }

        /// <summary>
        /// Gets or sets the collection of courses associated with the entity.
        /// </summary>
        public ICollection<Course>? Courses { get; set; }
    }
}
