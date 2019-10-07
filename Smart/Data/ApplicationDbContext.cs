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
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public new DbSet<User> Users { get; set; }
        public new DbSet<UserRole> UserRoles { get; set; }
        public new DbSet<Role> Roles { get; set; }
        public  DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<CourseTerm> CourseTerms { get; set; }
        public DbSet<CourseTermSchedule> CourseTermSchedules { get; set; }
        public DbSet<CourseTermInstructor> CourseTermInstructors { get; set; }
        public DbSet<StudentCourseTerm> StudentCourseTerms { get; set; }
        public DbSet<ApplicantRating> ApplicantRatings { get; set; }
        public DbSet<RatingCirterium> RatingCirteria { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<PublicSchoolCourseSchedule> PublicSchoolCourseSchedules { get; set; }
        public DbSet<StudentPublicSchoolCourse> StudentPublicSchoolCourses { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Attendance> Attendances { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Set up composite keys
            builder.Entity<CourseTermInstructor>().HasKey(c => new { c.CourseTermId, c.UserId });
            builder.Entity<StudentCourseTerm>().HasKey(s => new { s.CourseTermId, s.StudentId });
            builder.Entity<Attendance>().HasKey(a => new { a.StudentId, a.CourseTermId, a.Date }); // A student can only have attendance in a course once a day
            builder.Entity<CourseTermSchedule>().HasKey(c => new { c.CourseTermId, c.ScheduleId });
            builder.Entity<StudentAssessment>().HasKey(s => new { s.AssessmentId, s.StudentId }); // A student can only be assessed once for an assessment
            builder.Entity<UserRole>().HasKey(u => new { u.UserId, u.RoleId });

            // Set-up Enum conversions to byte (C#)/TinyInt (SQL Server)
            builder.Entity<Schedule>().Property(c => c.DayOfWeek).HasConversion(new EnumToNumberConverter<DayOfWeek, byte>());
            builder.Entity<Term>().Property(t => t.TimeOfYear).HasConversion(new EnumToNumberConverter<TimeOfYear, byte>());
            builder.Entity<StudentPublicSchoolCourse>().Property(s => s.TimeOfYear).HasConversion(new EnumToNumberConverter<TimeOfYear, byte>());
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
