using StudentHub.Core.Entities.Groups;
using StudentHub.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Core.Entities.Grades
{
    public class StudentGrade
    {
        public int Id { get; set; }

        public string StudentId { get; set; } = default!;

        public ApplicationUser Student { get; set; } = default!;

        public int CourseId { get; set; }
        public Course Course { get; set; } = default!;

        public int GroupId { get; set; }
        public Group Group { get; set; } = default!;
 
        public DateTime Date { get; set; }

        public int? Grade { get; set; } 

        public bool IsPresent { get; set; }
    }
}
