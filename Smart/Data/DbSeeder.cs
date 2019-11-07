using Microsoft.AspNetCore.Identity;
using Smart.Data.Models;
using Smart.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data
{
    public class DbSeeder
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public DbSeeder(RoleManager<Role> roleManager, UserManager<User> userManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
            _userManager = userManager;
        }

        public void SeedDatabase()
        {
            // User roles
            if (!_context.Roles.Any())
            {
                var roles = new Role[]
                {
                    new Role { Name = RoleEnum.Admin.GetDisplayName() },
                    new Role { Name = RoleEnum.Instructor.GetDisplayName() },
                    new Role { Name = RoleEnum.SocialWorker.GetDisplayName() },
                    new Role { Name = RoleEnum.Sponsor.GetDisplayName() },
                };

                foreach (var role in roles)
                {
                    _ = _roleManager.CreateAsync(role).Result;
                }
            }

            // Student statuses
            if (!_context.StudentStatuses.Any())
            {
                var studentStatuses = new StudentStatus[]
                {
                    new StudentStatus { StudentStatusId = StudentStatusEnum.Applicant, Description =   StudentStatusEnum.Applicant.GetDisplayName() },
                    new StudentStatus { StudentStatusId = StudentStatusEnum.Waitlisted, Description =  StudentStatusEnum.Waitlisted.GetDisplayName() },
                    new StudentStatus { StudentStatusId = StudentStatusEnum.Active, Description =      StudentStatusEnum.Active.GetDisplayName() },
                    new StudentStatus { StudentStatusId = StudentStatusEnum.Graduated, Description =   StudentStatusEnum.Graduated.GetDisplayName() },
                    new StudentStatus { StudentStatusId = StudentStatusEnum.Dropped, Description =     StudentStatusEnum.Dropped.GetDisplayName() }
                };

                _context.AddRange(studentStatuses);
                _context.SaveChanges();
            }

            // Attendance Statuses
            if (!_context.AttendanceStatuses.Any())
            {
                var attendanceStatuses = new AttendanceStatus[]
                {
                    new AttendanceStatus { AttendanceStatusId = AttendanceStatusEnum.OnTime, Description = AttendanceStatusEnum.OnTime.GetDisplayName() },
                    new AttendanceStatus { AttendanceStatusId = AttendanceStatusEnum.Late, Description =   AttendanceStatusEnum.Late.GetDisplayName() },
                    new AttendanceStatus { AttendanceStatusId = AttendanceStatusEnum.Absent, Description = AttendanceStatusEnum.Absent.GetDisplayName() },
                };

                _context.AttendanceStatuses.AddRange(attendanceStatuses);
                _context.SaveChanges();
            }

            //Test Rating Criteria
            if(!_context.RatingCirteria.Any())
            {
                var ratingCriteria = new RatingCirterium[]
                {
                    new RatingCirterium { RatingCirteriumId = 1, Description = "Poverty Score", MaxScore = 10 },
                    new RatingCirterium { RatingCirteriumId = 2, Description = "Academic Performance Score", MaxScore = 10 },
                    new RatingCirterium { RatingCirteriumId = 3, Description = "Test Score", MaxScore = 10 },
                };
            }

            // Schedule
            if (!_context.ScheduleAvailabilities.Any())
            {
                // Loop over each day of the week
                foreach (DayOfWeek dayOfWeek in Enum.GetValues(typeof(DayOfWeek)))
                {
                    // Loop over each hour of the day
                    for (int i = 0; i < 24; i++)
                    {
                        // Each schedule item is a one-hour block
                        _context.ScheduleAvailabilities.Add(new ScheduleAvailability() { DayOfWeek = dayOfWeek, StartTime = new TimeSpan(i, 0, 0), EndTime =  new TimeSpan(i == 23 ? 0 : i + 1, 0, 0) });
                        _context.SaveChanges(); // Save synchronously to preserver order
                    }
                }
            }

            // Courses
            if (!_context.Courses.Any())
            {
                var courses = new Course[]
                {
                    new Course { Name = "English Level 1", IsCoreRequirement = true },
                    new Course { Name = "English Level 2", IsCoreRequirement = true },
                    new Course { Name = "IT Level 1", IsCoreRequirement = true }
                };

                _context.Courses.AddRange(courses);
                _context.SaveChanges();
            }

            // Terms
            if (!_context.Terms.Any())
            {
                for (int year = 2020; year <= 2025; year++)
                {
                    foreach (TimeOfYear timeOfYear in Enum.GetValues(typeof(TimeOfYear)))
                    {
                        _context.Terms.Add(new Term() { TimeOfYear = timeOfYear, StartDate = Term.GetStartDate(timeOfYear, year), EndDate = Term.GetEndDate(timeOfYear, year) });
                    }
                    _context.SaveChanges(); // Save synchronously to preserver order
                }
            }

            // Add super user
            if (!_context.Users.Any())
            {
                var user = new User() { FirstName = "Admin", LastName = "Admin", Email = "Admin", UserName = "Admin" };
                _userManager.CreateAsync(user, "Secret123$").Wait();
                _userManager.AddToRoleAsync(user, RoleEnum.Admin.GetDisplayName()).Wait();
                _userManager.AddToRoleAsync(user, RoleEnum.Instructor.GetDisplayName()).Wait();
            }

            // Add sample student
            if (!_context.Students.Any())
            {
                _context.Students.Add(new Student()
                {
                    Address = "123 N Street",
                    LocationLattitude = -18.423459,
                    LocationLongitude = 35.631460,
                    DateOfBirth = DateTime.Today.AddYears(-15),
                    FirstName = "Sample",
                    LastName = "Student",
                    GuardianName = "Mom Sample",
                    Phone = "5551234567",
                    PublicSchoolLevel = 9,
                    Village = "Village",
                    StudentStatusId = StudentStatusEnum.Active,
                });
                _context.SaveChanges();
            }
        }
    }
}
