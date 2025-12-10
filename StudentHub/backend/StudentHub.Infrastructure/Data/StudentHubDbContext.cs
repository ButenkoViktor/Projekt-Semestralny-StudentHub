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

        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<ChatParticipant> ChatParticipants { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }

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
                .HasOne(m => m.User)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ChatParticipant: composite key
            modelBuilder.Entity<ChatParticipant>()
                .HasKey(cp => new { cp.ChatRoomId, cp.UserId });

            modelBuilder.Entity<ChatParticipant>()
                .HasOne(cp => cp.ChatRoom)
                .WithMany(cr => cr.Participants)
                .HasForeignKey(cp => cp.ChatRoomId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ChatParticipant>()
                .HasOne(cp => cp.User)
                .WithMany(u => u.ChatParticipants)
                .HasForeignKey(cp => cp.UserId)
                .OnDelete(DeleteBehavior.Cascade);


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
