using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentHub.Core.Entities.Announcements;
using StudentHub.Core.Entities.Chat;
using StudentHub.Core.Entities.Events;
using StudentHub.Core.Entities.Files;
using StudentHub.Core.Entities.Groups;
using StudentHub.Core.Entities.Identity;
using StudentHub.Core.Entities.Notes;
using StudentHub.Core.Entities.Notifications;
using StudentHub.Core.Entities.Schedule;
using StudentHub.Core.Entities.Tasks;
using StudentHub.Core.Entities.Grades;
using StudentHub.Infrastructure.Configurations;

namespace StudentHub.Infrastructure.Data
{
    public class StudentHubDbContext : IdentityDbContext<ApplicationUser>
    {
        public StudentHubDbContext(DbContextOptions<StudentHubDbContext> options)
            : base(options)
        {
        }

        // Groups & Courses
        public DbSet<Group> Groups { get; set; }
        public DbSet<Course> Courses { get; set; }

        // Announcements
        public DbSet<Announcement> Announcements { get; set; }

        // Chat
        public DbSet<ChatRoom> ChatRooms => Set<ChatRoom>();
        public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();

        // Events
        public DbSet<Event> Events { get; set; }

        // Files
        public DbSet<FileStorageRecord> Files { get; set; }

        // Notifications
        public DbSet<Notification> Notifications { get; set; }

        // Tasks
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<TaskAttachment> TaskAttachments { get; set; }
        public DbSet<TaskSubmission> TaskSubmissions { get; set; }
        public DbSet<TaskSubmissionFile> TaskSubmissionFiles { get; set; }
        public DbSet<TeacherCourseGroup> TeacherCourseGroups => Set<TeacherCourseGroup>();
        public DbSet<StudentGrade> StudentGrades => Set<StudentGrade>();

        // Schedule
        public DbSet<ScheduleItem> ScheduleItems { get; set; }

        // Notes
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply announcement configuration
            modelBuilder.ApplyConfiguration(new AnnouncementConfiguration());

            // Chat: Message User relationship
            modelBuilder.Entity<ChatMessage>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChatMessage>()
                .HasOne(m => m.ChatRoom)
                .WithMany(r => r.Messages)
                .HasForeignKey(m => m.ChatRoomId);


            // TaskSubmission -> TaskItem (1:n)
            modelBuilder.Entity<TaskSubmission>()
                .HasOne(ts => ts.Task)
                .WithMany(t => t.Submissions)
                .HasForeignKey(ts => ts.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            // TaskSubmissionFile -> TaskSubmission (1:n)
            modelBuilder.Entity<TaskSubmissionFile>()
                .HasOne(f => f.Submission)
                .WithMany(s => s.Files)
                .HasForeignKey(f => f.SubmissionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes: speed up queries
            modelBuilder.Entity<ScheduleItem>()
                .HasOne(s => s.Course)
                .WithMany(c => c.ScheduleItems)
                .HasForeignKey(s => s.CourseId);

            modelBuilder.Entity<TaskItem>()
                .HasIndex(t => t.Deadline);
        }
    }
}
