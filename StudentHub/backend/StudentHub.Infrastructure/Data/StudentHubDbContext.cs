using Microsoft.EntityFrameworkCore;
using StudentHub.Core.Entities.Users;
using StudentHub.Core.Entities.Chat;
using StudentHub.Core.Entities.Announcements;
using StudentHub.Core.Entities.Events;
using StudentHub.Core.Entities.Files;
using StudentHub.Core.Entities.Groups;
using StudentHub.Core.Entities.Notes;
using StudentHub.Core.Entities.Notifications;
using StudentHub.Core.Entities.Schedule;
using StudentHub.Core.Entities.Tasks;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StudentHub.Infrastructure.Identity;

namespace StudentHub.Infrastructure.Data
{
    // Data base context for StudentHub application
    public class StudentHubDbContext : IdentityDbContext<ApplicationUser>
    {
        public StudentHubDbContext(DbContextOptions<StudentHubDbContext> options)
            : base(options)
        {
        }

        // Users
        public DbSet<User> Users { get; set; }

        // Groups & Courses
        public DbSet<Group> Groups { get; set; }
        public DbSet<Course> Courses { get; set; }

        // Chat
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatParticipant> ChatParticipants { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

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

        // Announcements
        public DbSet<Announcement> Announcements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API configs if needed
        }
    }
}