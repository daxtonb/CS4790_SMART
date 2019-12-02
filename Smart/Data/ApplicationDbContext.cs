using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Smart.Data.Models;

namespace Smart.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<School> Schools { get; set; }
        public  DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<StudentMeeting> StudentMeetings { get; set; }
        public DbSet<StudentMeeting> StudentClasses { get; set; }
        public DbSet<ApplicantRating> ApplicantRatings { get; set; }
        public DbSet<RatingCirterium> RatingCirteria { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<ScheduleAvailability> ScheduleAvailabilities { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<NoteType> NoteTypes { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<AttendanceStatus> AttendanceStatuses { get; set; }
        public DbSet<StudentStatus> StudentStatuses { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<AssessmentType> AssessmentTypes { get; set; }
        public DbSet<StudentAssessment> StudentAssessments { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<FileType> FileTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Set up composite keys
            builder.Entity<StudentMeeting>().HasKey(s => new { s.MeetingId, s.StudentId }); // A student can only be enrolled to a meeting once
            builder.Entity<StudentAssessment>().HasKey(s => new { s.AssessmentId, s.StudentId }); // A student can only be assessed once for an assessment

            // Set up alternate keys
            builder.Entity<Meeting>().HasAlternateKey(m => new { m.ClassId, m.ScheduleAvailabilityId, m.MeetingOrderNum });    // Only one meeting can exist for a schedule availability
            builder.Entity<Class>().HasAlternateKey(c => new { c.CourseId, c.TermId }); // Only one class can exist for a course per term

            // Set up Enum conversions
            builder.Entity<ScheduleAvailability>().Property(c => c.DayOfWeek).HasConversion(new EnumToNumberConverter<DayOfWeek, byte>());
            builder.Entity<Student>().Property(s => s.StudentStatusId).HasConversion(new EnumToNumberConverter<StudentStatusEnum, int>());
            builder.Entity<StudentStatus>().Property(s => s.StudentStatusId).HasConversion(new EnumToNumberConverter<StudentStatusEnum, int>());
            builder.Entity<Attendance>().Property(s => s.AttendanceStatusId).HasConversion(new EnumToNumberConverter<AttendanceStatusEnum, int>());
            builder.Entity<AttendanceStatus>().Property(s => s.AttendanceStatusId).HasConversion(new EnumToNumberConverter<AttendanceStatusEnum, int>());
            builder.Entity<File>().Property(s => s.FileTypeId).HasConversion(new EnumToNumberConverter<FileTypeEnum, int>());
            builder.Entity<FileType>().Property(s => s.FileTypeId).HasConversion(new EnumToNumberConverter<FileTypeEnum, int>());

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId }); 
                
                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
        }

        public override int SaveChanges()
        {
            AddEventsAsync().Wait();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await AddEventsAsync();
            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Log the change and the responsible user
        /// </summary>
        private async Task AddEventsAsync()
        {
            // No user is logged in
            if (_httpContextAccessor.HttpContext == null || _httpContextAccessor.HttpContext.User == null)
                return;

            // Get the signed-in user who made the change(s)
            var user = await Users.FirstOrDefaultAsync(u => u.UserName == _httpContextAccessor.HttpContext.User.Identity.Name); // Name is actually the email, which is unique
            if (user == null)
                return;

            var events = new List<Event>();
            // Loop over tracked changes
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.State != EntityState.Unchanged)
                {
                    string description = $"{item.State} {item.Entity}:";
                    foreach (var property in item.Properties)
                    {
                        description += $" {property.Metadata.Name} = {(item.State == EntityState.Modified ? property.CurrentValue : property.OriginalValue) ?? "NULL"},";
                    }
                    description = description.TrimEnd(',');

                    // Log event
                    events.Add(new Event()
                    {
                        DateTime = DateTime.UtcNow,
                        UserId = user.Id,
                        Description = description
                    });
                }
            }

            Events.AddRange(events);
        }
    }
}
